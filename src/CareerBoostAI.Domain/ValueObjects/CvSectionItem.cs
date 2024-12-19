namespace CareerBoostAI.Domain.ValueObjects;

public class CvSectionItem
{
    
    private OrganisationName _organisationName;
    private Location _location;
    private CvSectionItemPosition _position;
    private CvSectionItemDescription _description;
    private CandidateCvSectionItemTimeRange _timeRange;

    public CvSectionItem(
        OrganisationName organisationName, 
        Location location, 
        CandidateCvSectionItemTimeRange timeRange, CvSectionItemPosition position, CvSectionItemDescription description)
    {
        _organisationName = organisationName;
        _location = location;
        _timeRange = timeRange;
        _position = position;
        _description = description;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not CvSectionItem other) return false;

        return _organisationName.Equals(other._organisationName)
               && _location.Equals(other._location)
               && _position.Equals(other._position)
               && _description.Equals(other._description)
               && _timeRange.Equals(other._timeRange);
    }
    
    public override int GetHashCode() =>
        HashCode.Combine(_organisationName, _location, _position, _description, _timeRange);
}