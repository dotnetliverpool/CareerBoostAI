using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.UploadContext.ValueObjects;

public class UploadFactory(IDateTimeProvider dateTimeProvider) : IUploadFactory
{
    public Upload Create(Guid id, string email, string address, string medium, string fileName, string extension)
    {
        var domainEmail = Email.Create(email);
        var document = Document.Create(
            address, medium, fileName, extension
        );
        return new (EntityId.Create(id), domainEmail, document, dateTimeProvider.UtcNow);
    }
}