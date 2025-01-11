using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public class CandidateFactoryTest : BaseCandidateTest
{
    [Fact]
    public void CandidateFactory_CreatesNewCandidate_When_Valid_Data_IsGiven()
    {
        var factory = GetCandidateFactory();
        CandidateAggregate candidate = factory.Create(
            FirstName.Create("John"),
            LastName.Create("Doe"),
            DateOfBirth.Create(new DateOnly(1998, 12, 13)),
            Email.Create("john@doe.com"),
            PhoneNumber.Create("+44", "812345678"),
            Enumerable.Empty<Cv>());
    }
    
}