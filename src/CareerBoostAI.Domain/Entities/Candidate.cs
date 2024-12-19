﻿using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class Candidate : AggregateRoot<CandidateId>
{
    public CandidateId Id { get; private set; }
    private CandidateFirstName _firstName;
    private CandidateLastName _lastName;
    private CandidateDOB _dateOfBirth;
    private List<CandidateEmail> _emails = new();
    private List<PhoneNumber> _phoneNumbers = new();
    private List<CandidateCv> _cvs = new();
    

    public Candidate(
        CandidateId id,
        CandidateFirstName firstName, 
        CandidateLastName lastName,
        CandidateDOB dateOfBirth)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateOfBirth = dateOfBirth;
    }

    
}