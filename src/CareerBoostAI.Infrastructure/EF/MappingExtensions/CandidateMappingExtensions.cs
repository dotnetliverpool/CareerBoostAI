using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Models;

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
                Code = candidate.PhoneNumber.Split(" ")[0],
                Number = candidate.PhoneNumber.Split(" ")[1],
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
            Skills = cv.Skills?.Select(cs => cs.Name) ?? [],
            Languages = cv.Languages?.Select(cl => cl.Name) ?? [],
            Experiences = cv.Experiences.Select(exp => exp.AsDto()),
            Educations = cv.Educations.Select(exp => exp.AsDto()),
            
        };
    }
    
    private static EducationDto AsDto(this Education education)
    {
        return new EducationDto
        {
            Id = education.Id,
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
            Id = experience.Id,
            OrganisationName = experience.OrganisationName,
            City = experience.City,
            Country = experience.Country,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Index = experience.Index,
            Description = experience.Description,
        };
    }
    
    
}
