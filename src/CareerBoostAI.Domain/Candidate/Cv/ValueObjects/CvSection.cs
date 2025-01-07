using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public sealed class CvSection(CvSectionName name, SequenceIndex sequenceIndex)
{
    
    private readonly List<CvSectionItem> _items = new();

    public SequenceIndex SequenceIndex { get; private set; } = sequenceIndex;
    public CvSectionName Name { get; private set; } = name;
    public IReadOnlyList<CvSectionItem> Items => _items.AsReadOnly();

    public void AddItem(CvSectionItem item)
    {
        throw new NotImplementedException();
    }
}