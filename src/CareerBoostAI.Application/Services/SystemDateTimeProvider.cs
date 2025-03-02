using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Application.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateOnly TodayAsDate 
        => DateOnly.FromDateTime(DateTime.Now);
}