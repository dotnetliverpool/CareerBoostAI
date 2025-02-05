using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Education = CareerBoostAI.Domain.CvContext.Entities.Education;

namespace CareerBoostAI.Domain.CvContext;

public class Cv : AggregateRoot<EntityId>
{
    private readonly List<Education> _educations;
    private readonly List<Experience> _experiences;
    private readonly List<Language> _languages;
    private readonly List<Skill> _skills;

    private Cv(EntityId id,
        Summary summary,
        Email candidateEmail,
        IEnumerable<Experience> experiences,
        IEnumerable<Education> educations,
        IEnumerable<Skill> skills,
        IEnumerable<Language> languages)
    {
        Id = id;
        Summary = summary;
        CandidateEmail = candidateEmail;
        _experiences = experiences.ToList();
        _educations = educations.ToList();
        _skills = skills.ToList();
        _languages = languages.ToList();
    }
    
    #pragma warning disable CS8618
    public Cv() { }

    public Summary Summary { get; private set; }
    public Email CandidateEmail { get; private set; }
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();

    internal static Cv Create(
        EntityId id, Summary summary,
        Email candidateEmail, IEnumerable<Experience> experiences,
        IEnumerable<Education> educations, IEnumerable<Skill> skills,
        IEnumerable<Language> languages)
    {
        var experienceList = experiences.ToArray();
        var educationList = educations.ToArray();

        return new Cv(id, summary, candidateEmail, experienceList,
            educationList, skills, languages);
    }

    public void UpdateSummary(string newSummary)
    {
        var result = Summary.Create(newSummary);
        if (Summary != result)
        {
            Summary = result;
        }
    }

    public void UpdateSkills(IEnumerable<string> dataSkills)
    {
        var newSkills = dataSkills.Select(Skill.Create).ToList();

        var skillsToRemove = _skills.Where(x => !newSkills.Contains(x));

        _skills.RemoveAll(skill => skillsToRemove.Contains(skill));

        foreach (var newSkill in newSkills)
            if (!_skills.Contains(newSkill))
                _skills.Add(newSkill);
    }

    public void UpdateLanguages(IEnumerable<string> dataLanguages)
    {
        var newLanguages = dataLanguages.Select(Language.Create).ToList();
        
        var languagesToRemove = _languages.Where(existingLanguage => !newLanguages.Contains(existingLanguage));
        
        _languages.RemoveAll(language => languagesToRemove.Contains(language));
        
        foreach (var newLanguage in newLanguages)
            if (!_languages.Contains(newLanguage))
                _languages.Add(newLanguage);
    }

    public void UpdateExperiences(IEnumerable<ExperienceData> dataExperiences)
    {
        var newExperiences = dataExperiences.Select(
            data => Experience.Create(
                Guid.NewGuid(), data.OrganisationName,
                data.City, data.Country, data.StartDate, data.EndDate,
                data.Description)).ToArray();
        _experiences.Clear();
        _experiences.AddRange(newExperiences);
    }

    public void UpdateEducations(IEnumerable<EducationData> dataEducations)
    {
        var newEducations = dataEducations.Select(
            data => Education.Create(
                Guid.NewGuid(), data.OrganisationName,
                data.City, data.Country, data.StartDate, data.EndDate,
                data.Program, data.Grade)).ToArray();
        _educations.Clear();
        _educations.AddRange(newEducations);
    }

    public bool HasExperienceAt(string company)
    {
        var orgName = OrganisationName.Create(company);
        var result = _experiences
            .FirstOrDefault(exp => exp.OrganisationName.Equals(orgName));
        return result is not null;
    }

    public bool HasEducationalBackgroundAt(string institution)
    {
        var orgName = OrganisationName.Create(institution);
        var result = _educations
            .FirstOrDefault(edu => edu.OrganisationName.Equals(orgName));
        return result is not null;
    }
}