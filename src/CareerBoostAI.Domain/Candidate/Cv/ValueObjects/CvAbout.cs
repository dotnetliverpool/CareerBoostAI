using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvAbout : ValueObject
{
    public string Value { get; }

    private CvAbout(string value)
    {
        Value = value;
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyArgumentException(nameof(CvAbout));
        }
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static CvAbout Create(string value)
    {
        Validate(value);
        return new CvAbout(value);
    }
}