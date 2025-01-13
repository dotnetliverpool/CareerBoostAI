using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Models;
using CvSection = CareerBoostAI.Infrastructure.EF.Models.CvSection;
using CvSectionItem = CareerBoostAI.Infrastructure.EF.Models.CvSectionItem;

namespace CareerBoostAI.Infrastructure.EF.MappingExtensions;

public static class CandidateMappinngExtensions
{
    // TODO: Entire Mapping is wrong (move back to mapping repository from domain object)
    public static CandidateDto AsDto(this Candidate candidate)
    {
        return new CandidateDto
        {
            Id = candidate.Id,
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            DateOfBirth = candidate.DateOfBirth,
            Email = candidate.Email,
            PhoneNumber = new PhoneNumberDto
            {
                Code = candidate.PhoneCode,
                Number = candidate.PhoneNumber,
            },
            Cv = candidate.Cv.AsDto()
        };
    }

   

    private static CvDto AsDto(this Cv cv)
    {
        return new CvDto
        {
            Id = cv.Id,
            Summary = cv.Summary,
            Skills = cv.CvSkills?.Select(cs => cs.Skill.Name) ?? [],
            Languages = cv.CvLanguages?.Select(cl => cl.Language.Name) ?? [],
            Experiences = cv.Experiences.Select(exp => exp.AsDto()),
            Educations = cv.Educations.Select(exp => exp.AsDto()),
            
        };
    }
    
    private static EducationDto AsDto(this Education education)
    {
        return new EducationDto
        {
            OrganisationName = education.OrganisationName,
            City = education.City,
            Country = education.Country,
            StartDate = education.StartDate,
            EndDate = education.EndDate,
            Index = education.Index,
            Program = education.Program,
            Grade = education.Grade
        };
    }

    private static ExperienceDto AsDto(this Experience experience)
    {
        return new ExperienceDto
        {
            OrganisationName = experience.OrganisationName,
            City = experience.City,
            Country = experience.Country,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Index = experience.Index,
            Description = experience.Description,
        };
    }
    
    public static Candidate AsModel(this CandidateDto candidateDto)
    {
        return new Candidate
        {
            Id = candidateDto.Id,
            FirstName = candidateDto.FirstName,
            LastName = candidateDto.LastName,
            DateOfBirth = candidateDto.DateOfBirth,
            Email = candidateDto.Email,
            PhoneCode = candidateDto.PhoneNumber.Code,
            PhoneNumber = candidateDto.PhoneNumber.Number,
            Cv = candidateDto.Cv.AsModel()
        };
    }

    private static Cv AsModel(this CvDto cvDto)
    {
        return new Cv
        {
            Id = cvDto.Id,
            Summary = cvDto.Summary,
            CvSkills = cvDto.Skills.Select(skill => new CvSkill
            {
                Skill = new Skill { Name = skill }
            }).ToList(),
            CvLanguages = cvDto.Languages.Select(language => new CvLanguage
            {
                Language = new Language { Name = language }
            }).ToList(),
            Experiences = cvDto.Experiences.Select(exp => exp.AsModel()).ToList(),
            Educations = cvDto.Educations.Select(edu => edu.AsModel()).ToList(),
        };
    }

    private static Education AsModel(this EducationDto educationDto)
    {
        return new Education
        {
            OrganisationName = educationDto.OrganisationName,
            City = educationDto.City,
            Country = educationDto.Country,
            StartDate = educationDto.StartDate,
            EndDate = educationDto.EndDate,
            Index = educationDto.Index,
            Program = educationDto.Program,
            Grade = educationDto.Grade,
        };
    }

    private static Experience AsModel(this ExperienceDto experienceDto)
    {
        return new Experience
        {
            OrganisationName = experienceDto.OrganisationName,
            City = experienceDto.City,
            Country = experienceDto.Country,
            StartDate = experienceDto.StartDate,
            EndDate = experienceDto.EndDate,
            Index = experienceDto.Index,
            Description = experienceDto.Description,
        };
    }
}
