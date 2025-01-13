using System.Collections.Specialized;
using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Application.Common.Extension;
using CareerBoostAI.Application.DTO;
using CareerBoostAI.Domain.Candidate;
using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.Enums;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Application.Candidate;

public static class CandidateDtoMappingExtensions
{
    public static CandidateDto AsDto(this CandidateAggregate candidateAggregate)
    {
        return new CandidateDto
        {
            Id = candidateAggregate.Id.Value,
            FirstName = candidateAggregate.FirstName.Value,
            LastName = candidateAggregate.LastName.Value,
            DateOfBirth = candidateAggregate.DateOfBirth.Value,
            Email = candidateAggregate.Email.Value,
            PhoneNumber = new PhoneNumberDto
            {
                Number = candidateAggregate.PhoneNumber.Number,
                Code = candidateAggregate.PhoneNumber.Code
            },
            Cv = candidateAggregate.Cv.AsDto(),
        };
    }
    public static CandidateAggregate AsDomain(
        this CandidateDto candidateDto, ICandidateFactory candidateFactory)
    {
        var cv = candidateDto.Cv.AsDomain(candidateFactory);
        return candidateFactory.Create(
            candidateDto.Id, candidateDto.FirstName, candidateDto.LastName, 
            candidateDto.DateOfBirth, candidateDto.Email, 
            candidateDto.PhoneNumber.Code, candidateDto.PhoneNumber.Number,
             cv
        );
    }
    
    private static CvDto AsDto(this Cv cv)
    {
        return new CvDto
        {
            Id = cv.Id.Value,
            Summary = cv.Summary.Value,
            Educations = cv.Educations.Select(edu => edu.AsDto()),
            Experiences = cv.Experiences.Select(exp => exp.AsDto()),
            Skills = cv.Skills.Select(sk => sk.Value),
            Languages = cv.Languages.Select(l => l.Value)
        };
    }

    private static EducationDto AsDto(this Education edu)
    {
        return new EducationDto
        {
            OrganisationName = edu.OrganisationName.Value,
            City = edu.Location.City,
            Country = edu.Location.Country,
            StartDate = edu.TimePeriod.StartDate,
            EndDate = edu.TimePeriod.EndDate,
            Program = edu.Grade.Program,
            Grade = edu.Grade.Grade,
            Index = edu.SequenceIndex.Value,

        };
    }
    
    private static ExperienceDto AsDto(this WorkExperience experience)
    {
        return new ExperienceDto
        {
            OrganisationName = experience.OrganisationName.Value,
            City = experience.Location.City,
            Country = experience.Location.Country,
            StartDate = experience.TimePeriod.StartDate,
            EndDate = experience.TimePeriod.EndDate,
            Description = experience.Description.Value,    
            Index = experience.SequenceIndex.Value,

        };
    }

   
    
    private static Cv AsDomain(this CvDto cvDto, ICandidateFactory candidateFactory)
    {
        return candidateFactory.CreateCv(
            cvDto.Id, cvDto.Summary,
            cvDto.Experiences
                .Select(exp => (
                    exp.OrganisationName, exp.City, exp.Country, exp.StartDate, exp.EndDate,
                    exp.Description, exp.Index)),
            cvDto.Educations
                .Select(edu => (
                edu.OrganisationName, edu.City, edu.Country, edu.StartDate, edu.EndDate,
                edu.Program, edu.Grade, edu.Index)),
            skills: cvDto.Skills,
            languages: cvDto.Languages
            );
    }
    
}