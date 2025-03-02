using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Infrastructure.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateOnly TodayAsDate 
        => DateOnly.FromDateTime(DateTime.Now);
    
    public DateTime UtcNow
        => DateTime.Now;
}