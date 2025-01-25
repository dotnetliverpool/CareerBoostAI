using CareerBoostAI.Domain.CandidateContext.Specifications;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Services;

namespace CareerBoostAI.Domain.CandidateContext.ValueObjects;

public class DateOfBirth : ValueObject
{
    public DateOnly Value { get; }

    // Constructor to ensure the date of birth is in the past
    private DateOfBirth(DateOnly value)
    {
        Value = value;
    }

    public static DateOfBirth Create(DateOnly value)
    {
        value.ThrowIfNull();
        return new DateOfBirth(value);
    }

    public static DateOfBirth Create(DateOnly value, IDateTimeProvider dateTimeProvider)
    {
        value.ThrowIfNull();
        var result =  new DateOfBirth(value);
        var spec = new AgeBetween10And120Specification(dateTimeProvider);
        if (!spec.IsSatisfiedBy(result))
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
        return result;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}