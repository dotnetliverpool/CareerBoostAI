using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Services;
using CareerBoostAI.Domain.ValueObjects;

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
        if (!AgeValidationService.IsWithinAcceptableAge(value, dateTimeProvider.TodayAsDate))
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
        return new DateOfBirth(value);
    }
    


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}