using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;



public abstract class BaseCandidateTest
{
    
    protected readonly IDateTimeProvider DateTimeProvider = new TestDateTimeProvider();
    protected CareerBoostAI.Domain.CandidateContext.Candidate GetNewValidCandidate()
    {
        var dateOfBirth = new DateOnly(1998, 12, 13);
        return new CareerBoostAI.Domain.CandidateContext.Candidate(
            EntityId.NewId(), Name.Create("John", "Doe"),
            DateOfBirth.Create(dateOfBirth, DateTimeProvider), 
            Email.Create("john.doe@gmail.com"), 
            PhoneNumber.Create("+44", "88436287893")
             );
    }

    protected ICandidateFactory GetCandidateFactory()
    {
        return new CandidateFactory(DateTimeProvider);
    }
}