using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class CvFile : ValueObject
{
    public Guid Value { get; private set; }
    
    private CvFile(Guid value)
    {
        Value = value;
    }

    public static CvFile Create(Guid value)
    {
        value.ThrowIfNull();
        return new CvFile(value);
    }
    
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
