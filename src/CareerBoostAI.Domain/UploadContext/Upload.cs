using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.UploadContext.ValueObjects;

namespace CareerBoostAI.Domain.UploadContext;

public class Upload : AggregateRoot<EntityId>
{
    public Email UserEmailAddress { get; private set; }
    public Document Document { get; private set; }
    
    public DateTime CreationDateTime { get; private set; }
    
    
    internal Upload(EntityId id, Email userEmailAddress, Document document,
        DateTime creationDateTime)
    {
        Id = id;
        Document = document;
        UserEmailAddress = userEmailAddress;
        CreationDateTime = creationDateTime;
    }
    
    public Upload() {}

    
}