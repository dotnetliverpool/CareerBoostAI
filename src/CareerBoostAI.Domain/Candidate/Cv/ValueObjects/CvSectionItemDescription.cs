using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSectionItemDescription : ValueObject
{
    public string Value { get; }

    public CvSectionItemDescription(string value)
    {
        Value = value;
    }

    public CvSectionItemDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(CvSectionItemDescription));
        }
        return new CvSectionItemDescription(value);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}
