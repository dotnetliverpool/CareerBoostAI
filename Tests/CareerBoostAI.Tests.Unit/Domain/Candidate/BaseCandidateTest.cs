using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;
using NSubstitute;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

internal class TestDateTimeProvider : IDateTimeProvider
{
    public DateOnly TodayAsDate => new DateOnly(2025, 1, 25);
    public DateTime Now => DateTime.Now;
}

public abstract class BaseCandidateTest
{
    
    protected readonly IDateTimeProvider DateTimeProvider = new TestDateTimeProvider();
    protected CareerBoostAI.Domain.CandidateContext.Candidate GetNewCandidateWithCv()
    {
        DateOnly dateOfBirth = new DateOnly(1998, 12, 13);
        return new (
            EntityId.NewId(), Name.Create("John", "Doe"),
            DateOfBirth.Create(dateOfBirth, DateTimeProvider.TodayAsDate), 
            Email.Create("john.doe@gmail.com"), 
            PhoneNumber.Create("+44", "88436287893")
             );
    }

    protected ICandidateFactory GetCandidateFactory()
    {
        return new CandidateFactory(DateTimeProvider);
    }
}