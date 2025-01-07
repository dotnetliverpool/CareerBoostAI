using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSectionItem
{

    private OrganisationName _organisationName;
    private SectionItemLocation _sectionItemLocation;
    private CvSectionItemSortNumber _sortNumber;
    private CvSectionItemDescription _description;
    private CandidateCvSectionItemTimeRange _timeRange;

    public CvSectionItem(
        OrganisationName organisationName,
        SectionItemLocation sectionItemLocation,
        CandidateCvSectionItemTimeRange timeRange, CvSectionItemSortNumber sortNumber,
        CvSectionItemDescription description)
    {
        _organisationName = organisationName;
        _sectionItemLocation = sectionItemLocation;
        _timeRange = timeRange;
        _sortNumber = sortNumber;
        _description = description;
    }

}