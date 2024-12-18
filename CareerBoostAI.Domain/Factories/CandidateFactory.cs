using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public class CandidateFactory
{
    Candidate Create(CandidateId id, CandidateFirstName firstName,
        CandidateLastName lastName, List<CandidateEmail> emails,
        CandidateDOB dateOfBirth, List<PhoneNumber> phoneNumbers,
        List<CandidateCV> candidateCvs)
    {
        var candidate =  new Candidate(id, firstName, lastName, dateOfBirth);
        // add emails, phone numbers and cvs
        return candidate;
    }
}