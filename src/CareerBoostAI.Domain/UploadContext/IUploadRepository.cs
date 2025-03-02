namespace CareerBoostAI.Domain.UploadContext;

public interface IUploadRepository
{
    public Task CreateNewAsync(Upload upload, CancellationToken cancellationToken);
}