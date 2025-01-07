using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionName
{
    public string Value { get; }

    public CvSectionName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyCvSectionName();
        Value = value;
    }
    
    public override string ToString() => Value;
}