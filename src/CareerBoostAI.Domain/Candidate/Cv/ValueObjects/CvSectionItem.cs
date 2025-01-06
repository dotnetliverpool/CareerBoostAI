namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionItem
{
    
    private OrganisationName _organisationName;
    private Location _location;
    private CvSectionItemSortNumber _sortNumber;
    private CvSectionItemDescription _description;
    private CandidateCvSectionItemTimeRange _timeRange;

    public CvSectionItem(
        OrganisationName organisationName, 
        Location location, 
        CandidateCvSectionItemTimeRange timeRange, CvSectionItemSortNumber sortNumber, CvSectionItemDescription description)
    {
        _organisationName = organisationName;
        _location = location;
        _timeRange = timeRange;
        _sortNumber = sortNumber;
        _description = description;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not CvSectionItem other) return false;

        return _organisationName.Equals(other._organisationName)
               && _location.Equals(other._location)
               && _sortNumber.Equals(other._sortNumber)
               && _description.Equals(other._description)
               && _timeRange.Equals(other._timeRange);
    }
    
    public override int GetHashCode() =>
        HashCode.Combine(_organisationName, _location, _sortNumber, _description, _timeRange);
}