using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.UploadContext.ValueObjects;

namespace CareerBoostAI.Domain.UploadContext;

public class Upload : AggregateRoot<EntityId>
{
    public Email UserEmailAddress { get; private set; }
    public Document Document { get; private set; }
    
    public DateTime CreationDateTime { get; private set; }
    
    
    public Upload(EntityId id, Email userEmailAddress, Document document,
        DateTime creationDateTime)
    {
        Id = id;
        Document = document;
        UserEmailAddress = userEmailAddress;
        CreationDateTime = creationDateTime;
    }

    public static Upload Create(Guid id, string email, string address, 
        string medium, string fileName, string extension, DateTime creationDateTime)
    {
        var domainEmail = Email.Create(email);
        var document = Document.Create(
            address, medium, fileName, extension
        );
        return new (EntityId.Create(id), domainEmail, document, creationDateTime);


    }

    
}