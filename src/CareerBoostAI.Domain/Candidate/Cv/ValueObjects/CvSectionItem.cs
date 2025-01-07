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
        CandidateCvSectionItemTimeRange timeRange, CvSectionItemSortNumber sortNumber,
        CvSectionItemDescription description)
    {
        _organisationName = organisationName;
        _location = location;
        _timeRange = timeRange;
        _sortNumber = sortNumber;
        _description = description;
    }

}