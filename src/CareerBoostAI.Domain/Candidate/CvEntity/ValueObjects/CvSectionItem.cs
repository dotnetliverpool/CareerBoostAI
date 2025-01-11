using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public class CvSectionItem(
    OrganisationName organisationName,
    SectionItemLocation sectionItemLocation,
    Period timeRange,
    CvSectionItemDescription description,
    SequenceIndex sequenceIndex)
{
    public OrganisationName OrganisationName { get; private set; } = organisationName;
    public SectionItemLocation Location { get; private set; } = sectionItemLocation;
    public Period TimeRange { get; private set; } = timeRange;
    public CvSectionItemDescription Description { get; private set; } = description;
    public SequenceIndex SequenceIndex { get; private set; } = sequenceIndex;
}