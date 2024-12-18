using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public sealed class CandidateFactory : ICandidateFactory
{
    public Candidate Create(CandidateId id, CandidateFirstName firstName,
        CandidateLastName lastName, List<CandidateEmail> emails,
        CandidateDOB dateOfBirth, List<PhoneNumber> phoneNumbers,
        List<CandidateCV> candidateCvs)
    {
        var candidate =  new Candidate(id, firstName, lastName, dateOfBirth);
        // TODO : add emails, phone numbers and cvs
        return candidate;
    }
}