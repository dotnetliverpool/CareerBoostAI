using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateFactory
{
    public  Domain.Candidate.Candidate Create(CandidateId id, FirstName firstName,
        LastName lastName, List<Email> emails,
        DateOfBirth dateOfBirth, List<PhoneNumber> phoneNumbers
        );
}