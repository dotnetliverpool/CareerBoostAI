using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CandidateCvSectionItemTimeRange : ValueObject
{
    
    private static  DateOnly NoEndDate => DateOnly.MinValue;
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public bool IsOngoing { get; private set; } = false;

    private CandidateCvSectionItemTimeRange(DateOnly startDate, DateOnly? endDate = null)
    {
        StartDate = startDate;
        EndDate = endDate;
        IsOngoing = !EndDate.HasValue;
    }

    private static void Validate(DateOnly startDate, DateOnly? endDate)
    {
        if (endDate.HasValue && endDate < startDate)
        {
            throw new CvSectionItemEndDateEarlierThanStartDateException();
        }
    }

    public void SetOngoing()
    {
        EndDate = null;
        IsOngoing = true;
    }

    public void SetEndDate(DateOnly endDate)
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
        yield return EndDate ?? NoEndDate;
        yield return IsOngoing;
    }

    public static CandidateCvSectionItemTimeRange Create(DateOnly startDate, DateOnly? endDate)
    {
        Validate(startDate, endDate);
        return new CandidateCvSectionItemTimeRange(startDate, endDate);
    }
}