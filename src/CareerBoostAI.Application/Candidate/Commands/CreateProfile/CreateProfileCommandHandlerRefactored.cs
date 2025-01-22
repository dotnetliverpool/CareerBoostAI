using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

    public class CreateProfileCommandHandlerRefactored : ICommandHandler<CreateProfileCommand>
    {
        
        private readonly ICandidateRepositoryR _candidateRepository;
        private readonly IEmailSender _emailSender;
        private readonly ICandidateFactory _candidateFactory;
        private readonly ICvFactory _cvFactory;
        private readonly ICvRepository _cvRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICandidateReadService _candidateReadService;

        public CreateProfileCommandHandlerRefactored(
            IFileStorageService fileStorageService, 
            IEmailSender emailSender, ICandidateRepositoryR candidateRepository, 
            ICandidateReadService candidateReadService,
            ICandidateFactory candidateFactory, IUnitOfWork unitOfWork, 
            ICvFactory cvFactory, ICvRepository cvRepository)
        {
            _emailSender = emailSender;
            _candidateRepository = candidateRepository;
            _candidateReadService = candidateReadService;
            _candidateFactory = candidateFactory;
            _unitOfWork = unitOfWork;
            _cvFactory = cvFactory;
            _cvRepository = cvRepository;
        }
        public async Task Handle(CreateProfileCommand command, CancellationToken cancellationToken)
        { 
            if (await _candidateReadService.CandidateExistsByEmailAsync(command.Email, cancellationToken))
            {
                throw new DuplicateCandidateProfileException(command.Email);
            }
            
            var candidateProfile = _candidateFactory
                    .Create(Guid.NewGuid(), command.FirstName, 
                        command.LastName, command.DateOfBirth, 
                        command.Email, command.PhoneCode, command.PhoneNumber);
            var cv = _cvFactory.CreateFromData(
                    Guid.NewGuid(),
                    command.CreateCvCommand.AsDomainCvData(command.Email));
            
            
            await _candidateRepository.CreateNewAsync(candidateProfile);
            await _cvRepository.CreateNewAsync(cv);
                //  send profile created notification
                // send cv created notification
            await _unitOfWork.SaveChangesAsync(cancellationToken); 
        }

    
    
}