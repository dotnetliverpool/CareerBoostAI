﻿using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;
using CareerBoostAI.Domain.CvContext.Factory;
using CvData = CareerBoostAI.Domain.CvContext.Factory.CvData;

namespace CareerBoostAI.Application.Candidate;

public static class CandidateDtoMappingExtensions
{

    public static CvData AsDomainCvData(this CreateCvCommand command)
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
                Description = exp.Description
            }),
            Educations = command.Educations.Select(edu => new EducationData
            {
                OrganisationName = edu.OrganisationName,
                City = edu.City,
                Country = edu.Country,
                StartDate = edu.StartDate,
                EndDate = edu.EndDate,
                Program = edu.Program,
                Grade = edu.Grade
            }),
            Skills = command.Skills,
            Languages = command.Languages
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
                Description = exp.Description
            }),
            Educations = command.Educations.Select(edu => new EducationData
            {
                OrganisationName = edu.OrganisationName,
                City = edu.City,
                Country = edu.Country,
                StartDate = edu.StartDate,
                EndDate = edu.EndDate,
                Program = edu.Program,
                Grade = edu.Grade
            }),
            Skills = command.Skills,
            Languages = command.Languages
        };
    }
    
}