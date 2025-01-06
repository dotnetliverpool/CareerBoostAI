using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvAbout : ValueObject
{
    public string Value { get; }

    public CvAbout(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new EmptyArgumentException(nameof(CvAbout));

        Value = value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}