﻿using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate;

public class CandidateAggregate : AggregateRoot<CandidateId>
{
    

    #region  Properties
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public CvEntity.Cv Cv { get; private set; }
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    

    #endregion
    
    internal CandidateAggregate(
        CandidateId id,
        FirstName firstName, LastName lastName,
        DateOfBirth dateOfBirth, Email email, 
        PhoneNumber phoneNumber,  CvEntity.Cv cv)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        Cv = cv;
    }
    
    
}