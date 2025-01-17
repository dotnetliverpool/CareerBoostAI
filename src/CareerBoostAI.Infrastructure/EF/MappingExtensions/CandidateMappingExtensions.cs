using CareerBoostAI.Application.Candidate.DTO;
using CareerBoostAI.Infrastructure.EF.Models;

namespace CareerBoostAI.Infrastructure.EF.MappingExtensions;

public static class CandidateMappinngExtensions
{
    // TODO: Entire Mapping is wrong (move back to mapping repository from domain object)
    public static CandidateDto AsDto(this CandidateReadModel candidateReadModel)
    {
        return new CandidateDto
        {
            Id = candidateReadModel.Id,
            FirstName = candidateReadModel.FirstName,
            LastName = candidateReadModel.LastName,
            DateOfBirth = candidateReadModel.DateOfBirth,
            Email = candidateReadModel.Email,
            PhoneNumber = new PhoneNumberDto
            {
                Code = candidateReadModel.PhoneNumber.Split(" ")[0],
                Number = candidateReadModel.PhoneNumber.Split(" ")[1],
            },
            Cv = candidateReadModel.CvReadModel.AsDto()
        };
    }

   

    private static CvDto AsDto(this CvReadModel cvReadModel)
    {
        return new CvDto
        {
            Id = cvReadModel.Id,
            Summary = cvReadModel.Summary,
            Skills = cvReadModel.Skills?.Select(cs => cs.Name) ?? [],
            Languages = cvReadModel.Languages?.Select(cl => cl.Name) ?? [],
            Experiences = cvReadModel.Experiences.Select(exp => exp.AsDto()),
            Educations = cvReadModel.Educations.Select(exp => exp.AsDto()),
            
        };
    }
    
    private static EducationDto AsDto(this EducationReadModel educationReadModel)
    {
        return new EducationDto
        {
            Id = educationReadModel.Id,
            OrganisationName = educationReadModel.OrganisationName,
            City = educationReadModel.City,
            Country = educationReadModel.Country,
            StartDate = educationReadModel.StartDate,
            EndDate = educationReadModel.EndDate,
            Index = educationReadModel.SequenceIndex,
            Program = educationReadModel.Program,
            Grade = educationReadModel.Grade
        };
    }

    private static ExperienceDto AsDto(this ExperienceReadModel experienceReadModel)
    {
        return new ExperienceDto
        {
            Id = experienceReadModel.Id,
            OrganisationName = experienceReadModel.OrganisationName,
            City = experienceReadModel.City,
            Country = experienceReadModel.Country,
            StartDate = experienceReadModel.StartDate,
            EndDate = experienceReadModel.EndDate,
            Index = experienceReadModel.SequenceIndex,
            Description = experienceReadModel.Description,
        };
    }
    
    
}
