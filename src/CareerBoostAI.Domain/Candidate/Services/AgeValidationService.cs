using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Domain.Candidate.Services;

public static class AgeValidationService
{
    // Use Specification Pattern Instead to explicitly describe behaviour
    
    private static int CalculateAge(DateOnly birthDate, DateOnly today)
    {
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }
        
        return age;
    }

    public static void IsWithinAcceptableAge(DateOnly birthDate, DateOnly today)
    {
        var age = CalculateAge(birthDate, today);
        var isWithinAcceptedRange=  age is > 12 and < 120;
        if (!isWithinAcceptedRange)
        {
            throw new AgeNotWithinAcceptedRangeException();
        }
    }

}