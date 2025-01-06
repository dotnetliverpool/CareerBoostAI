

using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

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