using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;

namespace CareerBoostAI.Domain.Candidate.CvEntity;

public abstract class ProfessionalEntry : Entity<ProfessionalEntryId>
{
    protected ProfessionalEntry(
        ProfessionalEntryId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        SequenceIndex sequenceIndex)
    {
        Id = id;
        OrganisationName = organisationName;
        Location = location;
        TimePeriod = timePeriod;
        SequenceIndex = sequenceIndex;
    }

    public OrganisationName OrganisationName { get; }
    public Location Location { get; }
    public Period TimePeriod { get; }
    public SequenceIndex SequenceIndex { get; }
    
}



