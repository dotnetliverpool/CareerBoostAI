namespace CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;


public sealed class UploadCvDocumentCommandHandler
{
    private readonly IUserReadService _userReadService;
    private readonly IFileUploadService _fileUploadService;
    private readonly IUploadRepository _uploadRepository;

    public UploadCvDocumentCommandHandler(
        IUserReadService userReadService, 
        IFileUploadService fileUploadService,
        IUploadRepository uploadRepository)
    {
        _userReadService = userReadService ?? throw new ArgumentNullException(nameof(userReadService));
        _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
        _uploadRepository = uploadRepository ?? throw new ArgumentNullException(nameof(uploadRepository));
    }

    public async Task<string> HandleAsync(UploadCvDocumentCommand command, CancellationToken cancellationToken)
    {
        // Step 1: Check if user exists
        var userExists = await _userReadService.UserExistsAsync(command.Email, cancellationToken);
        if (!userExists)
        {
            throw new InvalidOperationException("Must upload profile data first.");
        }

        // Step 2: Upload the file using the file upload service
        var storageAddress = await _fileUploadService.UploadFileAsync(
            command.DocumentName, 
            command.DocumentContent, 
            command.ContentType, 
            cancellationToken);

        // Step 3: Create the upload entity
        var upload = new UploadEntity
        {
            StorageMedium = "CloudStorage", // Example storage medium
            StorageAddress = storageAddress,
            UserEmail = command.Email,
            DocumentName = command.DocumentName,
            UploadedAt = DateTime.UtcNow
        };

        // Step 4: Save the upload entity
        await _uploadRepository.AddAsync(upload, cancellationToken);

        // Step 5: Return the storage address
        return storageAddress;
    }
}
