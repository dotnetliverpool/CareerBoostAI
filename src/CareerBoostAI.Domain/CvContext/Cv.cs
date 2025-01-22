using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Education = CareerBoostAI.Domain.CvContext.Entities.Education;

namespace CareerBoostAI.Domain.CvContext;

public class Cv : AggregateRoot<EntityId>
{
    private List<Experience> _experiences;
    private List<Education> _educations;
    private List<Skill> _skills;
    private List<Language> _languages;
    
    
    public Summary Summary { get; private set; }
    public Email CandidateEmail { get; private set; }
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
    
    internal Cv(EntityId id,
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
}