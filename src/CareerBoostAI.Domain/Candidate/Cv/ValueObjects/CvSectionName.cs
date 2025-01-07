using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSectionName
{
    public string Value { get; }

    private CvSectionName(string value)
    {
        Value = value;
    }

    public static CvSectionName Create(string value)
    {
        Validate(value);
        return new(value);
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(CvSectionName));
        }
    }
    public override string ToString() => Value;
}