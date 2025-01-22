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
    public static CandidateDto AsDto(this CandidateProfile candidateProfile)
    {
        return new CandidateDto
        {
            Id = candidateProfile.Id.Value,
            FirstName = candidateProfile.FirstName.Value,
            LastName = candidateProfile.LastName.Value,
            DateOfBirth = candidateProfile.DateOfBirth.Value,
            Email = candidateProfile.Email.Value,
            PhoneNumber = new PhoneNumberDto
            {
                Number = candidateProfile.PhoneNumber.Number,
                Code = candidateProfile.PhoneNumber.Code
            },
            Cv = candidateProfile.CandidateCv.AsDto(),
        };
    }
    
    private static CvDto AsDto(this CandidateCv candidateCv)
    {
        return new CvDto
        {
            Id = candidateCv.Id.Value,
            Summary = candidateCv.Summary.Value,
            Educations = candidateCv.Educations.Select(edu => edu.AsDto()),
            Experiences = candidateCv.Experiences.Select(exp => exp.AsDto()),
            Skills = candidateCv.Skills.Select(sk => sk.Value),
            Languages = candidateCv.Languages.Select(l => l.Value)
        };
    }

    private static EducationDto AsDto(this Education edu)
    {
        return new EducationDto
        {
            Id = edu.Id.Value,
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
            Id = experience.Id.Value,
            OrganisationName = experience.OrganisationName.Value,
            City = experience.Location.City,
            Country = experience.Location.Country,
            StartDate = experience.TimePeriod.StartDate,
            EndDate = experience.TimePeriod.EndDate,
            Description = experience.Description.Value,    
            Index = experience.SequenceIndex.Value,

        };
    }

   
    
    private static CandidateCv AsDomain(this CvDto cvDto, ICandidateFactory candidateFactory)
    {
        return candidateFactory.CreateCv(
            cvDto.Id, cvDto.Summary,
            cvDto.Experiences
                .Select(exp => (exp.Id,
                    exp.OrganisationName, exp.City, exp.Country, exp.StartDate, exp.EndDate,
                    exp.Description, exp.Index)),
            cvDto.Educations
                .Select(edu => (edu.Id,
                edu.OrganisationName, edu.City, edu.Country, edu.StartDate, edu.EndDate,
                edu.Program, edu.Grade, edu.Index)),
            skills: cvDto.Skills,
            languages: cvDto.Languages
            );
    }
    
}