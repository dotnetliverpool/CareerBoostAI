using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

    public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
    {

        private readonly ICandidateReadService _candidateReadService;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ICandidateFactory _candidateFactory;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProfileCommandHandler(
            IFileStorageService fileStorageService, 
            IEmailSender emailSender, ICandidateRepository candidateRepository, 
            ICandidateReadService candidateReadService,
            ICandidateFactory candidateFactory, IUnitOfWork unitOfWork)
        {
            _emailSender = emailSender;
            _candidateRepository = candidateRepository;
            _candidateReadService = candidateReadService;
            _candidateFactory = candidateFactory;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(CreateProfileCommand command, CancellationToken cancellationToken)
        {
            await Validate(command, cancellationToken);
            
            var candidate = CreateAggregateFromCommand(command);
            
            await _candidateRepository.CreateNewAsync(candidate);
            
            var adminNotificationMessage =
                $"A new candidate profile has been created for {candidate.FirstName.Value} {candidate.LastName.Value}.";
            await _emailSender.SendEmailToAdminAsync(subject: "New Candidate Profile Created", body: adminNotificationMessage);
            
            
            await _unitOfWork.SaveChangesAsync(cancellationToken); 
        }

    private CandidateProfile CreateAggregateFromCommand(CreateProfileCommand request)
    {
        var cv = _candidateFactory.CreateCv(
            request.Id, request.CvData.Summary,
            request.CvData.Experiences
                .Select(exp => ( Guid.NewGuid(),
                    exp.OrganisationName, exp.City, exp.Country,
                    exp.StartDate, exp.EndDate, exp.Description,
                    exp.SequenceIndex)),
            request.CvData.Educations
                .Select(edu => (Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                    edu.StartDate, edu.EndDate, edu.Program, edu.Grade,
                    edu.SequenceIndex)),
            request.CvData.Languages,
            request.CvData.Skills);

        CandidateProfile candidate = _candidateFactory
            .Create(Guid.NewGuid(), request.FirstName, 
                request.LastName, request.DateOfBirth, 
                request.Email, request.PhoneCode, request.PhoneNumber, 
                cv);
        return candidate;
    }

    private async Task Validate(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (await _candidateReadService.CandidateExistsByEmailAsync(request.Email, cancellationToken))
        {
            throw new DuplicateCandidateProfileException(request.Email);
        }
    }
}