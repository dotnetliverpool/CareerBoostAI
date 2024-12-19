using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class CvSection
{
    private readonly CvSectionSortNumber _sortNumber;
    private readonly CvSectionName _name;
    private readonly List<CvSectionItem> _items = new();

    public CvSectionSortNumber SortNumber => _sortNumber;
    public CvSectionName Name => _name; 
    public IReadOnlyList<CvSectionItem> Items => _items.AsReadOnly(); 

    public CvSection(CvSectionName name, CvSectionSortNumber sortNumber)
    {
        _name = name;
        _sortNumber = sortNumber;
    }

    public void AddItem(CvSectionItem item)
    {
        if (_items.Any(existing => existing.Equals(item)))
            throw new DuplicateSectionItemException();
        _items.Add(item);
    }
}