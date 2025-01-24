using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.CandidateContext;

public class Candidate : AggregateRoot<EntityId>
{
    #region  Properties
    public Name Name {get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    #endregion
    
    internal Candidate(
        EntityId id,
        Name name,
        DateOfBirth dateOfBirth, Email email, 
        PhoneNumber phoneNumber)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
}