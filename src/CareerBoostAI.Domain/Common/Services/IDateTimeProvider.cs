namespace CareerBoostAI.Domain.Common.Services;

public interface IDateTimeProvider
{
   DateOnly TodayAsDate { get; }
   
   DateTime UtcNow { get;  }
}