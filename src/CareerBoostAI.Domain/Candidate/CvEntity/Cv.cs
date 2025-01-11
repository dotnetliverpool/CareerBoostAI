using System.Data;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;

namespace CareerBoostAI.Domain.Candidate.CvEntity;

public sealed class Cv(CvId id, CvFile file, BaseCvContent? content = null) : Entity<CvId>
{
    private List<Experience> _experiences;
    private List<Education> _educations;
    private List<Skill> _skills;
    private List<Language> _languages;
    
    
    public Summary Summary { get; private set; }
    public IReadOnlyCollection<Experience> Experiences => _experiences.AsReadOnly();
    public IReadOnlyCollection<Education> Educations => _educations.AsReadOnly();
    public IReadOnlyCollection<Skill> Skills => _skills.AsReadOnly();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
    public CvFile File { get; private set; } = file;
    public BaseCvContent Content { get; private set; } = content ?? NullCvContent.Instance;

    public bool IsParsed => Content is not NullCvContent;

    public void SetContent(BaseCvContent content)
    {
        if (Content is not NullCvContent)
        {
            throw new InvalidExpressionException("Cv content cannot be changed");
        }
        Content = content;
    }
    
}