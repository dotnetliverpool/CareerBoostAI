﻿using System.Collections.Specialized;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.ValueObjects;
using CvData = CareerBoostAI.Domain.CvContext.Factory.CvData;
using Education = CareerBoostAI.Domain.Candidate.CvEntity.Education;

namespace CareerBoostAI.Application.Candidate;

public static class CandidateDtoMappingExtensions
{

    public static CvData AsDomainCvData(this CreateCvCommand command, string candidateEmail)
    {
        return new CvData
        {
            Summary = command.Summary,
            Experiences = command.Experiences.Select(exp => new ExperienceData
            {
                OrganisationName = exp.OrganisationName,
                City = exp.City,
                Country = exp.Country,
                StartDate = exp.StartDate,
                EndDate = exp.EndDate,
                Description = exp.Description,
                Index = exp.SequenceIndex
            }),
            Educations = command.Educations.Select(edu => new EducationData
            {
                OrganisationName = edu.OrganisationName,
                City = edu.City,
                Country = edu.Country,
                StartDate = edu.StartDate,
                EndDate = edu.EndDate,
                Program = edu.Program,
                Grade = edu.Grade,
                Index = edu.SequenceIndex
            }),
            Skills = command.Skills,
            Languages = command.Languages,
            CandidateEmail = candidateEmail
        };
    }
    
    public static CvData AsDomainCvData(this UpdateCvCommand command)
    {
        return new CvData
        {
            Summary = command.Summary,
            Experiences = command.Experiences.Select(exp => new ExperienceData
            {
                OrganisationName = exp.OrganisationName,
                City = exp.City,
                Country = exp.Country,
                StartDate = exp.StartDate,
                EndDate = exp.EndDate,
                Description = exp.Description,
                Index = exp.SequenceIndex
            }),
            Educations = command.Educations.Select(edu => new EducationData
            {
                OrganisationName = edu.OrganisationName,
                City = edu.City,
                Country = edu.Country,
                StartDate = edu.StartDate,
                EndDate = edu.EndDate,
                Program = edu.Program,
                Grade = edu.Grade,
                Index = edu.SequenceIndex
            }),
            Skills = command.Skills,
            Languages = command.Languages,
            CandidateEmail = command.Email
        };
    }
    
}