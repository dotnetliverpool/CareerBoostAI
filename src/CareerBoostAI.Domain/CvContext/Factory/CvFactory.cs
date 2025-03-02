using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.Factory;

public class CvFactory : ICvFactory
{
    public Cv CreateFromData(string candidateEmail, CvData data)
    {
        var domainSummary = Summary.Create(data.Summary);
        var domainCandidateEmail = Email.Create(candidateEmail);
        var domainExperiences = data.Experiences
            .Select(exp => Experience.Create(
                Guid.NewGuid(),
                exp.OrganisationName, 
                exp.City, 
                exp.Country, 
                exp.StartDate, 
                exp.EndDate, exp.Description));
        var domainEducations = data.Educations
            .Select(edu =>
                Education.Create(Guid.NewGuid(), edu.OrganisationName,
                    edu.City,
                    edu.Country,
                    edu.StartDate,
                    edu.EndDate,
                    edu.Program,
                    edu.Grade
                ));
        var domainSkills = data.Skills
            .Select(Skill.Create);
        var domainLanguages = data.Languages
            .Select(Language.Create);
        
        return Cv.Create(EntityId.NewId(), domainSummary, domainCandidateEmail, domainExperiences, 
            domainEducations, domainSkills, domainLanguages);
    }
    
}