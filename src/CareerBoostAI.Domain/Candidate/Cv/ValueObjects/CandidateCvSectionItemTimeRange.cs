using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CandidateCvSectionItemTimeRange : ValueObject
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
        {
            throw new CvSectionItemEndDateEarlierThanStartDateException();
        }

        EndDate = endDate;
        IsOngoing = false;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return StartDate;
        yield return EndDate;
        yield return IsOngoing;
    }
}