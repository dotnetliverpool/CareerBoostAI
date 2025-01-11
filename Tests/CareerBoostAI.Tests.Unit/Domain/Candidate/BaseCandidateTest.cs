using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using NSubstitute;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public abstract class BaseCandidateTest
{
    protected CareerBoostAI.Domain.Candidate.CandidateAggregate GetNewCandidate()
    {
        DateOnly dateOfBirth = new DateOnly(1998, 12, 13);
        return new (
            CandidateId.New(), FirstName.Create("John"), 
            LastName.Create("Doe"),
            DateOfBirth.Create(dateOfBirth));
    }

    protected ICandidateFactory GetCandidateFactory()
    {
        return new CandidateFactory();
    }
}