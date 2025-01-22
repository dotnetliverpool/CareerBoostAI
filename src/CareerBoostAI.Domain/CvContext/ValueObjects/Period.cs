using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.ValueObjects;

public class Period : ValueObject
{
    
    private static  DateOnly NoEndDate => DateOnly.MinValue;
    public DateOnly StartDate { get;  }
    public DateOnly? EndDate { get;  }

    public bool IsOngoing
    {
        get => EndDate is null;
    }

    private Period(DateOnly startDate, DateOnly? endDate = null)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    private static void Validate(DateOnly startDate, DateOnly? endDate)
    {
        startDate.ThrowIfNull();
        if (endDate.HasValue && endDate < startDate)
        {
            throw new CvSectionItemEndDateEarlierThanStartDateException();
        }
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return StartDate;
        yield return EndDate ?? NoEndDate;
        yield return IsOngoing;
    }

    public static Period Create(DateOnly startDate, DateOnly? endDate)
    {
        Validate(startDate, endDate);
        return new Period(startDate, endDate);
    }
}