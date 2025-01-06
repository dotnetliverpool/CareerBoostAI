using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public sealed class CandidateFactory : ICandidateFactory
{
    public Candidate Create(CandidateId id, FirstName firstName,
        LastName lastName, List<Email> emails,
        DateOfBirth dateOfBirth, List<PhoneNumber> phoneNumbers
        )
    {
        var candidate =  new Candidate(id, firstName, lastName, dateOfBirth);
        // TODO : add emails, phone numbers and cvs
        return candidate;
    }
}