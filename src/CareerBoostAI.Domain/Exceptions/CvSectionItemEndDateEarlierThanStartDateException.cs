namespace CareerBoostAI.Domain.Exceptions;

public class CvSectionItemEndDateEarlierThanStartDateException : CareerBoostAIDomainException
{
    public CvSectionItemEndDateEarlierThanStartDateException() : base("EndDate cannot be earlier than StartDate.")
    {
    }
}