using CareerBoostAI.Domain.Common.Exceptions;

namespace CareerBoostAI.Domain.Services;

public static class AgeValidationService
{
    private static int CalculateAge(DateOnly birthDate, DateOnly today)
    {
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }
        
        return age;
    }

    public static bool IsWithinAcceptableAge(DateOnly birthDate, DateOnly today)
    {
        var age = CalculateAge(birthDate, today);
        return  age is > 12 and < 120;
    }

}