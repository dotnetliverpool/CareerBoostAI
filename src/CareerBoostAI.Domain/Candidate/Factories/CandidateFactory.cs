

using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public sealed class CandidateFactory : ICandidateFactory
{
    public Candidate Create(FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth, 
        Email email, PhoneNumber phoneNumber
        )
    {
        var candidate =  new Candidate(CandidateId.New(), firstName, lastName, dateOfBirth);
        candidate.AddEmail(email);
        candidate.AddPhoneNumber(phoneNumber);
        return candidate;
    }

    public Candidate Create(CandidateId id, FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth,
        List<Email> emails, List<PhoneNumber> phoneNumbers, List<Cv.Cv> cvs)
    {
        var candidate =  new Candidate(id, firstName, lastName, dateOfBirth);
        candidate.AddEmails(emails);
        candidate.AddPhoneNumbers(phoneNumbers);
        candidate.AddCvs(cvs);
        return candidate;
    }
}