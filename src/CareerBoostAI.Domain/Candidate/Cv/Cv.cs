using System.Data;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv;

public class Cv(CvId id, CvFile file, BaseCvContent? content = null) : Entity<CvId>
{
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

    public void AddSection(CvSection section)
    {
        Content.AddSection(section);    
    }
}