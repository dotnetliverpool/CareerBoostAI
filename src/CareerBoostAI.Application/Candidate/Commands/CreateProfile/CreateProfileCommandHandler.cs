using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

    public class CreateProfileCommandHandler(
        ICandidateReadService candidateReadService,
        ICandidateRepository candidateRepository,
        ICandidateFactory candidateFactory,
        ICvRepository cvRepository,
        ICvFactory cvFactory,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender)
        : ICommandHandler<CreateProfileCommand, Guid>
    {
        private readonly IEmailSender _emailSender = emailSender;

        public async Task<Guid> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
        { 
            if (await candidateReadService.CandidateExistsByEmailAsync(command.Email, cancellationToken))
            {
                throw new CandidateProfileAlreadyExistsException(command.Email);
            }
            var candidate = candidateFactory
                    .Create( command.FirstName, 
                        command.LastName, command.DateOfBirth, 
                        command.Email, command.PhoneCode, command.PhoneNumber);
            var cv = cvFactory.CreateFromData(command.Email,
                    command.CreateCvCommand.AsDomainCvData());
            
            await candidateRepository.CreateNewAsync(candidate);
            await cvRepository.CreateNewAsync(cv);
                //  send profile created notification
                // send cv created notification
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return candidate.Id.Value;
        }

    
    
}