﻿using CareerBoostAI.Domain.Entities;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Factories;

public interface ICandidateFactory
{
    Candidate Create(CandidateId id, CandidateFirstName firstName,
        CandidateLastName lastName, List<CandidateEmail> emails,
        CandidateDOB dateOfBirth, List<PhoneNumber> phoneNumbers,
        List<CandidateCV> candidateCvs);
}