using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext;

public class Candidate : AggregateRoot<EntityId>
{
    #region  Properties
    public FirstName FirstName { get; }
    public LastName LastName { get;  }
    public DateOfBirth DateOfBirth { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    #endregion
    
    internal Candidate(
        EntityId id,
        FirstName firstName, LastName lastName,
        DateOfBirth dateOfBirth, Email email, 
        PhoneNumber phoneNumber)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
}