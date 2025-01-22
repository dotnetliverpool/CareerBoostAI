using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext.Factories;

public sealed class CandidateFactory(IDateTimeProvider dateTimeProvider) : ICandidateFactory
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    
   

    public Candidate Create(
        Guid id, string firstName, string lastName, DateOnly dateOfBirth, string email,
        string phoneCode, string phoneNumber)
    {
        
        var candidateId = CandidateId.Create(id);
        var domainFirstName = FirstName.Create(firstName);
        var domainLastName = LastName.Create(lastName);
        var domainEmail = Email.Create(email);
        var domainDateOfBirth = DateOfBirth.Create(dateOfBirth, _dateTimeProvider);
        var domainPhone = PhoneNumber.Create(phoneCode, phoneNumber);
        
        return new(
            candidateId, domainFirstName, 
            domainLastName, domainDateOfBirth,
            domainEmail, domainPhone);
    }

   
}