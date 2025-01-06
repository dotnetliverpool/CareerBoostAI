using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public interface ICandidateFactory
{
    public  Candidate Create(CandidateId id, FirstName firstName,
        LastName lastName, List<Email> emails,
        DateOfBirth dateOfBirth, List<PhoneNumber> phoneNumbers
        );
}