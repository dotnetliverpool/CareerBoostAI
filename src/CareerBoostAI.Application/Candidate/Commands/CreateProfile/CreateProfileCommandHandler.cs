using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Common.Exceptions;
using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand>
{

    private readonly ICandidateReadService _candidateReadService;
    private readonly ICandidateRepository _candidateRepository;
    private readonly IEmailSender _emailSender;
    private readonly ICandidateFactory _candidateFactory;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProfileCommandHandler(
        IFileStorageService fileStorageService, IMediator mediator, 
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
        await _unitOfWork.SaveChangesAsync(cancellationToken); 
        
        var adminNotificationMessage =
            $"A new candidate profile has been created for {candidate.FirstName.Value} {candidate.LastName.Value}.";
        await _emailSender.SendEmailToAdminAsync(subject: "New Candidate Profile Created", body: adminNotificationMessage);
    }

    private CandidateAggregate CreateAggregateFromCommand(CreateProfileCommand request)
    {
        var cv = _candidateFactory.CreateCv(
            request.Id, request.Data.CvData.Summary,
            request.Data.CvData.Experiences
                .Select(exp => ( Guid.NewGuid(),
                    exp.OrganisationName, exp.City, exp.Country,
                    exp.StartDate, exp.EndDate, exp.Description,
                    exp.SequenceIndex)),
            request.Data.CvData.Educations
                .Select(edu => (Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                    edu.StartDate, edu.EndDate, edu.Program, edu.Grade,
                    edu.SequenceIndex)),
            request.Data.CvData.Languages,
            request.Data.CvData.Skills);

        CandidateAggregate candidate = _candidateFactory
            .Create(Guid.NewGuid(), request.Data.FirstName, 
                request.Data.LastName, request.Data.DateOfBirth, 
                request.Data.Email, request.Data.PhoneCode, request.Data.PhoneNumber, 
                cv);
        return candidate;
    }

    private async Task Validate(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        if (await _candidateReadService.CandidateExistsByEmailAsync(request.Data.Email, cancellationToken))
        {
            throw new DuplicateCandidateProfileException(request.Data.Email);
        }
    }
}