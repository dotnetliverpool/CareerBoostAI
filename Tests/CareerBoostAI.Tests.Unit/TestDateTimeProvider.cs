using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Tests.Unit;

internal class TestDateTimeProvider : IDateTimeProvider
{
    public DateOnly TodayAsDate => new DateOnly(2025, 1, 25);
    public DateTime UtcNow => new DateTime(2025, 1, 1, 12, 0, 0);
}