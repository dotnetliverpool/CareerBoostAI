

using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public sealed class CandidateFactory : ICandidateFactory
{
    public CandidateAggregate Create(FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth, 
        Email email, PhoneNumber phoneNumber
        )
    {
        var candidate =  new CandidateAggregate(CandidateId.New(), firstName, lastName, dateOfBirth);
        candidate.RegisterEmail(email);
        candidate.RegisterPhoneNumber(phoneNumber);
        return candidate;
    }

    public CandidateAggregate HydrateCreate(CandidateId id, FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth,
        List<Email> emails, List<PhoneNumber> phoneNumbers, List<Cv.Cv> cvs)
    {
        var candidate =  new CandidateAggregate(id, firstName, lastName, dateOfBirth);
        candidate.AddEmails(emails);
        candidate.AddPhoneNumbers(phoneNumbers);
        candidate.AddCvs(cvs);
        return candidate;
    }
}