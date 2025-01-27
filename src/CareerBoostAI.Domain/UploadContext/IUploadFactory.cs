namespace CareerBoostAI.Domain.UploadContext;

public interface IUploadFactory
{
    public Upload Create(Guid id, string email, string address,
        string medium, string fileName, string extension);
}