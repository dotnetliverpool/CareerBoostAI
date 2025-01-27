using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Common.Exceptions;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.UploadContext;

namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;


public sealed class UploadCvDocumentCommandHandler(
    IDocumentConstraintsService documentConstraintsService,
    IFileStorageService fileStorageService,
    IUploadRepository uploadRepository,
    ICandidateReadService candidateReadService,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<UploadCvDocumentCommand>
{
    public async Task Handle(UploadCvDocumentCommand command, CancellationToken cancellationToken)
    {
        await ValidateAsync(command, cancellationToken);
        
        var uploadResult = await fileStorageService.UploadFileAsync(
            StorageContainer.Cv,
            command.DocumentStream,
            command.DocumentName,
            cancellationToken);
        
        var upload = Upload.Create(
            uploadResult.Id, command.Email, 
            uploadResult.Address,
            uploadResult.StorageMedium.ToString(),  
            uploadResult.OriginalName, uploadResult.FileExtension,
            dateTimeProvider);

        
        await uploadRepository.CreateNewAsync(upload, cancellationToken);
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
            throw new UnsupportedFileTypeException(documentConstraintsService.GetSupportedFileTypes());
        }

        if (!documentConstraintsService.SizeWithinLimit(command.DocumentStream))
        {
            throw new DocumentExceedsMaximumUploadSizeException(
                documentConstraintsService.GetMaxSizeInFormat(DocumentSizeFormat.Mb));
        }
    }
}
