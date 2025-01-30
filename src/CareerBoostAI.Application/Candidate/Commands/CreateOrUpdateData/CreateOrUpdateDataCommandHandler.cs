using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Domain.CandidateContext;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

    public class CreateProfileCommandHandler(
        ICandidateReadService candidateReadService,
        ICandidateRepository candidateRepository,
        ICandidateFactory candidateFactory,
        ICvRepository cvRepository,
        ICvUpdateService cvUpdateService,
        ICvFactory cvFactory,
        IUnitOfWork unitOfWork,
        IEmailSender emailSender)
        : ICommandHandler<CreateOrUpdateProfileCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrUpdateProfileCommand command, CancellationToken cancellationToken)
        { 
            Domain.CandidateContext.Candidate candidate;
            if (await candidateReadService.CandidateExistsByEmailAsync(command.Email, cancellationToken))
            {
                 candidate = await UpdateExistingCandidateProfile(command, cancellationToken);
            }
            else
            {
                candidate = await CreateNewCandidateProfile(command, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return candidate.Id.Value;
        }

        private async Task<Domain.CandidateContext.Candidate> UpdateExistingCandidateProfile(
            CreateOrUpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var candidate = await candidateRepository
                .GetByEmailAsync(command.Email, cancellationToken);
            var cv = await cvRepository
                .GetByEmailAsync(candidate!.Email.Value, cancellationToken);
            // Update Candidate
            
            cvUpdateService.Update(cv!, command.CreateCvCommand.AsDomainCvData());
            await candidateRepository.UpdateAsync(candidate, cancellationToken);
            await cvRepository.UpdateAsync(cv!, cancellationToken);
            return candidate;
        }

        private async Task<Domain.CandidateContext.Candidate> CreateNewCandidateProfile(
            CreateOrUpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var candidate = candidateFactory
                .Create( 
                    command.FirstName, command.LastName, 
                    command.DateOfBirth, command.Email, 
                    command.PhoneCode, command.PhoneNumber);
            
            var cv = cvFactory.CreateFromData(
                command.Email,
                command.CreateCvCommand.AsDomainCvData());
            
            await candidateRepository.CreateNewAsync(candidate, cancellationToken);
            await cvRepository.CreateNewAsync(cv, cancellationToken);
            return candidate;
        }
    }