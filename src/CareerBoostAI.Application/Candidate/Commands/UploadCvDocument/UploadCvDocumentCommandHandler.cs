using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Application.Common.Abstractions.Mediator;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.UploadContext;

namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;


public sealed class UploadCvDocumentCommandHandler : ICommandHandler<UploadCvDocumentCommand>
{
    private readonly ICandidateReadService _candidateReadService;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUploadRepository _uploadRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public UploadCvDocumentCommandHandler( 
        IFileStorageService fileStorageService,
        IUploadRepository uploadRepository, 
        ICandidateReadService candidateReadService, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
    {
        
        _fileStorageService = fileStorageService;
        _uploadRepository = uploadRepository;
        _candidateReadService = candidateReadService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(UploadCvDocumentCommand command, CancellationToken cancellationToken)
    {
        if (!await _candidateReadService.CandidateExistsByEmailAsync(command.Email, cancellationToken))
        {
            // TODO : Use Custom Application Exception
            throw new InvalidOperationException("Must upload profile data first.");
        }
        
        var uploadResult = await _fileStorageService.UploadFileAsync(
            StorageContainer.Cv,
            command.DocumentStream,
            command.DocumentName,
            cancellationToken);
        
        var upload = Upload.Create(
            uploadResult.Id, command.Email, 
            uploadResult.Address,
            uploadResult.StorageMedium.ToString(),  
            uploadResult.OriginalName, uploadResult.FileExtension,
            _dateTimeProvider.Now);

        
        await _uploadRepository.CreateNewAsync(upload, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
