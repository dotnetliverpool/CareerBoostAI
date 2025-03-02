using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext.Factories;

public sealed class CandidateFactory(IDateTimeProvider dateTimeProvider) : ICandidateFactory
{
    public Candidate Create(
         string firstName, string lastName, DateOnly dateOfBirth, string email,
        string phoneCode, string phoneNumber)
    {
        
        var candidateId = EntityId.NewId();
        var candidateName = Name.Create(firstName, lastName);
        var domainEmail = Email.Create(email);
        var domainDateOfBirth = DateOfBirth.Create(dateOfBirth, dateTimeProvider);
        var domainPhone = PhoneNumber.Create(phoneCode, phoneNumber);
        
        return new Candidate(
            candidateId, candidateName, domainDateOfBirth,
            domainEmail, domainPhone);
    }

   
}