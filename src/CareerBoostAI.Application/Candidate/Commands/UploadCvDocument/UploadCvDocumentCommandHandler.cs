using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Abstractions.Transaction;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Domain.UploadContext;

namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;


public sealed class UploadCvDocumentCommandHandler(
    IDocumentConstraintsService documentConstraintsService,
    IStorageService storageService,
    IUploadFactory uploadFactory,
    IUploadRepository uploadRepository,
    ICandidateReadService candidateReadService,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UploadCvDocumentCommand>
{
    public async Task Handle(UploadCvDocumentCommand command, CancellationToken cancellationToken)
    {
        await ValidateAsync(command, cancellationToken);
        
        
        var uploadResult = await storageService.UploadFileAsync(
            StorageContainer.Cv,
            command.DocumentStream,
            command.DocumentName,
            cancellationToken);
        
        var upload = uploadFactory.Create(
            uploadResult.Id, command.Email, 
            uploadResult.Address,
            uploadResult.StorageMedium.ToString(),  
            uploadResult.OriginalName, uploadResult.FileExtension);

        
        await uploadRepository.CreateNewAsync(upload, cancellationToken);
        
        unitOfWork.RegisterRollBackAction(
            storageService.GetUploadRollBackAction(uploadResult.Address), cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task ValidateAsync(UploadCvDocumentCommand command, CancellationToken cancellationToken)
    {
        if (!await candidateReadService.CandidateExistsByEmailAsync(command.Email, cancellationToken))
        {
            throw new CandidateProfileNotFoundException(command.Email);
        }

        if (!documentConstraintsService.SupportsDocumentType(command.DocumentName))
        {
            throw new UnsupportedFileTypeException(documentConstraintsService.GetSupportedFileTypes()
                .Select(ft => ft.ToString()));
        }

        if (!documentConstraintsService.SizeWithinLimit(command.DocumentStream.Length))
        {
            throw new DocumentSizeOutOfBoundsException(
                documentConstraintsService.GetMaxSizeInFormat(DocumentSizeFormat.Mb));
        }
    }
}
