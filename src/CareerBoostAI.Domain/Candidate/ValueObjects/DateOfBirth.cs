using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.ValueObjects;

public class DateOfBirth : ValueObject
{
    public DateTime Value { get; }

    // Constructor to ensure the date of birth is in the past
    private DateOfBirth(DateTime value)
    {
        Value = value;
    }

    public static DateOfBirth Create(DateTime value)
    {
        var age = CalculateAge(value);

        
        if (age < 10 || age > 120)
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
        return new DateOfBirth(value);
    }
    
    private static int CalculateAge(DateTime birthDate)
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