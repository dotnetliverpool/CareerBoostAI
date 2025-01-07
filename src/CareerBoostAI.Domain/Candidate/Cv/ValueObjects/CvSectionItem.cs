using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSectionItem
{

    private OrganisationName _organisationName;
    private SectionItemLocation _sectionItemLocation;
    private CvSectionItemDescription _description;
    private CandidateCvSectionItemTimeRange _timeRange;
    private SequenceIndex _sequenceIndex;

    public CvSectionItem(
        OrganisationName organisationName,
        SectionItemLocation sectionItemLocation,
        CandidateCvSectionItemTimeRange timeRange,
        CvSectionItemDescription description,
        SequenceIndex sequenceIndex)
    {
        _organisationName = organisationName;
        _sectionItemLocation = sectionItemLocation;
        _timeRange = timeRange;
        _description = description;
        _sequenceIndex = sequenceIndex;
    }

}