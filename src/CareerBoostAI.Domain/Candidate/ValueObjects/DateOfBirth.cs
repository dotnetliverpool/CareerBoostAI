using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

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
        var age = CalculateAge(value);

        
        if (age < 10 || age > 120)
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
        return new DateOfBirth(value);
    }

    public static DateOfBirth CreateTrusted(DateOnly value)
    {
        return new DateOfBirth(value);
    }
    
    private static int CalculateAge(DateOnly birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        return age;
    }


    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}