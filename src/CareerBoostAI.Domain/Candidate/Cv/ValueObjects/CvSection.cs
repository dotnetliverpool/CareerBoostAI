using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSection
{
    private readonly SequenceIndex _sequenceIndex;
    private readonly CvSectionName _name;
    private readonly List<CvSectionItem> _items = new();

    public SequenceIndex SequenceIndex => _sequenceIndex;
    public CvSectionName Name => _name; 
    public IReadOnlyList<CvSectionItem> Items => _items.AsReadOnly(); 

    public CvSection(CvSectionName name, SequenceIndex sequenceIndex)
    {
        _name = name;
        _sequenceIndex = sequenceIndex;
    }

    public void AddItem(CvSectionItem item)
    {
        throw new NotImplementedException();
    }
}