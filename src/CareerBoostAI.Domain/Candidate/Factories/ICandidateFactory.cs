﻿using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public interface ICandidateFactory
{
    public CandidateProfile Create(Guid id, string firstName,
        string lastName, DateOnly dateOfBirth,
        string email, string phoneCode, string phoneNumber, 
         CvEntity.CandidateCv candidateCv);
    
    public CvEntity.CandidateCv CreateCv(
        Guid id,  string summary,
        IEnumerable<(Guid id, string orgName, string city, string country, 
            DateOnly startDate, DateOnly? endDate, string description, uint index)> experiences, 
        IEnumerable<(Guid id, string orgName, string city, string country, 
            DateOnly startDate, DateOnly? endDate, string program, string grade, uint index)> educations,
        IEnumerable<string> languages, IEnumerable<string> skills);

}