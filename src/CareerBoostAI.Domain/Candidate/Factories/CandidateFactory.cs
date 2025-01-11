using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public sealed class CandidateFactory : ICandidateFactory
{

    public CandidateAggregate Create(
        CandidateId id,
        FirstName firstName, LastName lastName, 
        DateOfBirth dateOfBirth, Email email,
        PhoneNumber phoneNumber, IEnumerable<Cv.Cv>? cvs)
    {
        var cvsList = cvs?.ToList() ?? new List<Cv.Cv>();
        ValidateInputNotNull(firstName, lastName, dateOfBirth, email, phoneNumber, cvsList);
        cvsList.ThrowIfContainsDuplicates<CvId>();
        
        var result =  new CandidateAggregate(
            id, firstName, lastName, dateOfBirth, email, phoneNumber, cvsList);
        return result;
    }

    private  void ValidateInputNotNull(FirstName firstName, LastName lastName, DateOfBirth dateOfBirth, Email email,
        PhoneNumber phoneNumber, IEnumerable<Cv.Cv> cvs)
    {
        firstName.ThrowIfNull();
        lastName.ThrowIfNull();
        dateOfBirth.ThrowIfNull();
        email.ThrowIfNull();
        phoneNumber.ThrowIfNull();
        foreach (var cv in cvs)
        {
            cv.ThrowIfNull();
        }
    }

   
}