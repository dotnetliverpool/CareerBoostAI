using System.Data;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;

namespace CareerBoostAI.Domain.Candidate.CvEntity;

public sealed class CandidateCv : Entity<CvId>
{
    private List<WorkExperience> _experiences;
    private List<Education> _educations;
    private List<Skill> _skills;
    private List<Language> _languages;
    
    
    public Summary Summary { get; private set; }
    public IReadOnlyCollection<WorkExperience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();

    internal CandidateCv(CvId id,
        Summary summary,
        IEnumerable<WorkExperience> experiences, 
        IEnumerable<Education> educations, 
        IEnumerable<Skill> skills, 
        IEnumerable<Language> languages)
    {
        Id = id;
        Summary = summary;
        _experiences = experiences.ToList();
        _educations = educations.ToList();
        _skills = skills.ToList();
        _languages = languages.ToList();
    }
    
}