using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.Factory;

public class CvFactory : ICvFactory
{
    public Cv CreateFromData(CvData data)
    {
        var cvId = EntityId.NewId();
        var domainSummary = Summary.Create(data.Summary);
        var domainCandidateEmail = Email.Create(data.CandidateEmail);
        var domainExperiences = data.Experiences
            .Select(exp => Experience.Create(
                exp.Id,
                exp.OrganisationName, 
                exp.City, 
                exp.Country, 
                exp.StartDate, 
                exp.EndDate, exp.Description,
                exp.Index));
        var domainEducations = data.Educations
            .Select(edu =>
                Education.Create(edu.Id, edu.OrganisationName,
                    edu.City,
                    edu.Country,
                    edu.StartDate,
                    edu.EndDate,
                    edu.Program,
                    edu.Grade,
                    edu.Index
                ));
        var domainSkills = data.Skills
            .Select(Skill.Create);
        var domainLanguages = data.Languages
            .Select(Language.Create);
        
        return new(cvId, domainSummary, domainCandidateEmail, domainExperiences, 
            domainEducations, domainSkills, domainLanguages);
    }
}