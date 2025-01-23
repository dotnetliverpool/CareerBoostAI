using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext.Factories;

public sealed class CandidateFactory(IDateTimeProvider dateTimeProvider) : ICandidateFactory
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    
   

    public Candidate Create(
         string firstName, string lastName, DateOnly dateOfBirth, string email,
        string phoneCode, string phoneNumber)
    {
        
        var candidateId = EntityId.NewId();
        var domainFirstName = FirstName.Create(firstName);
        var domainLastName = LastName.Create(lastName);
        var domainEmail = Email.Create(email);
        var domainDateOfBirth = DateOfBirth.Create(dateOfBirth, _dateTimeProvider.TodayAsDate);
        var domainPhone = PhoneNumber.Create(phoneCode, phoneNumber);
        
        return new Candidate(
            candidateId, domainFirstName, 
            domainLastName, domainDateOfBirth,
            domainEmail, domainPhone);
    }

   
}