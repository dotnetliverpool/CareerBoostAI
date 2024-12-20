using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateDOB : ValueObject
{
    public DateTime Value { get; }

    // Constructor to ensure the date of birth is in the past
    private CandidateDOB(DateTime value)
    {
        Value = value;
    }

    public static CandidateDOB Create(DateTime value)
    {
        var age = CalculateAge(value);

        
        if (age < 10 || age > 120)
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
        return new CandidateDOB(value);
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