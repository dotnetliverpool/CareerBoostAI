using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Services;

namespace CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;

public class UpdateCvCommandHandler(
    ICvRepository cvRepository,
    ICvUpdateService cvUpdateService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateCvCommand>
{
    public async Task Handle(UpdateCvCommand command, CancellationToken cancellationToken)
    {
        var cv = await cvRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (cv is null)
        {
            throw new CandidateCvNotFoundException(command.Email);
        }

        cvUpdateService.Update(cv!, command.AsDomainCvData());
        await cvRepository.UpdateAsync(cv!, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

    }
}