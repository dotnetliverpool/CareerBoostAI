﻿using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateFactory
{
    public  Domain.Candidate.Candidate Create(FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth, 
        Email email, PhoneNumber phoneNumber
        );
    
    public  Domain.Candidate.Candidate Create(CandidateId id, FirstName firstName,
        LastName lastName, DateOfBirth dateOfBirth,
        List<Email> emails, List<PhoneNumber> phoneNumbers, List<Cv.Cv> cvs
    );
}