using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions.SpecificationPattern;
using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Domain.CandidateContext.Specifications;

public class AgeBetween10And120Specification(IDateTimeProvider dateTimeProvider) : Specification<DateOfBirth>
{
    private int CalculateAge(DateOnly birthDate, DateOnly today)
    {
        var age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age))
        {
            age--;
        }
        
        return age;
    }
    
    public override bool IsSatisfiedBy(DateOfBirth candidate)
    {
        var age = CalculateAge(candidate.Value, dateTimeProvider.TodayAsDate);
        return  age is > 12 and < 120;
    }
}