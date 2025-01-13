using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.UserUpload.ValueObjects;

public class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }
    
        
    public static UserId Create(Guid id)
    {
        id.ThrowIfNull();
        return new (id);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}