using CareerBoostAI.Domain.Common.Services;

namespace CareerBoostAI.Tests.Unit;

internal class TestDateTimeProvider : IDateTimeProvider
{
    private DateOnly _todayAsDateHelper = new DateOnly(2025, 1, 25);
    private DateTime _utcNowHelper = new DateTime(2025, 1, 1, 12, 0, 0);
    public DateOnly TodayAsDate => _todayAsDateHelper;
    public DateTime UtcNow => _utcNowHelper;

    public static TestDateTimeProvider FromDateString(string dateString)
    {
        if (string.IsNullOrWhiteSpace(dateString))
        {
            throw new ArgumentException("Date string cannot be null or empty.", nameof(dateString));
        }

        if (!DateOnly.TryParse(dateString, out var parsedDate))
        {
            throw new ArgumentException("Invalid date string format.", nameof(dateString));
        }

        return new TestDateTimeProvider { _todayAsDateHelper = parsedDate };
    }

    public static TestDateTimeProvider WithUtcNowAs(uint year, uint month, uint day, uint hour, uint minute,
        uint second)
    {
        var date = new DateTime((int)year, (int)month, (int)day, (int)hour, (int)minute, (int)second);
        return new TestDateTimeProvider { _utcNowHelper = date };
    }
}
