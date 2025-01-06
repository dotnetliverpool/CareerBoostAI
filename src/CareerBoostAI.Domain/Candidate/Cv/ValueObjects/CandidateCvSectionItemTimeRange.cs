using CareerBoostAI.Domain.Exceptions;

namespace CareerBoostAI.Domain.ValueObjects;

public class CandidateCvSectionItemTimeRange
{
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsOngoing { get; private set; } = false;

    public CandidateCvSectionItemTimeRange(DateTime startDate, DateTime? endDate = null)
    {
        if (endDate.HasValue && endDate < startDate)
        {
            throw new CvSectionItemEndDateEarlierThanStartDateException();
        }
        
        StartDate = startDate;
        EndDate = endDate;
        IsOngoing = !EndDate.HasValue;
    }

    public void SetOngoing()
    {
        EndDate = null;
        IsOngoing = true;
    }

    public void SetEndDate(DateTime endDate)
    {
        if (endDate < StartDate)
            throw new CvSectionItemEndDateEarlierThanStartDateException();

        EndDate = endDate;
        IsOngoing = false;
    }

    public override string ToString()
    {
        return IsOngoing
            ? $"{StartDate:yyyy-MM-dd} to Present"
            : $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
    }
}