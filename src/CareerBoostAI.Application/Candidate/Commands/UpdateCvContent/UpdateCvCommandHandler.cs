using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.Services;

namespace CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;

public class UpdateCvCommandHandler : ICommandHandler<UpdateCvCommand>
{
    private readonly ICvRepository _cvRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCvCommandHandler(ICvRepository cvRepository, IUnitOfWork unitOfWork)
    {
        _cvRepository = cvRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateCvCommand command, CancellationToken cancellationToken)
    {
        var cv = await _cvRepository.GetByEmailAsync(command.Email);
        if (cv is null)
        {
            throw new CandidateCvNotFoundException(command.Email);
        }

        CvInformationUpdateService.Update(cv!, command.AsDomainCvData());
        await _cvRepository.UpdateAsync(cv!);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

    }
}