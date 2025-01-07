using System.Data;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv;

public class Cv
{
    public CvId Id { get; private set; }
    public CvFile File { get; private set; }
    public BaseCvContent Content { get; private set; }

    public bool IsParsed => Content is not NullCvContent;

    public Cv(CvId id, CvFile file, BaseCvContent? content = null)
    {
        Id = id;
        File = file;
        Content = content ?? NullCvContent.Instance;
    }

    public void SetContent(BaseCvContent content)
    {
        if (Content is not NullCvContent)
        {
            throw new InvalidExpressionException("Cv content cannot be changed");
        }
        Content = content;
    }
}