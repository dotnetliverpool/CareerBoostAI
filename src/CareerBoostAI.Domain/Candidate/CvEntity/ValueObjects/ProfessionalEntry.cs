using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public abstract class ProfessionalEntry : ValueObject
{
    protected ProfessionalEntry(
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        SequenceIndex sequenceIndex)
    {
        OrganisationName = organisationName;
        Location = location;
        TimePeriod = timePeriod;
        SequenceIndex = sequenceIndex;
    }

    public OrganisationName OrganisationName { get; }
    public Location Location { get; }
    public Period TimePeriod { get; }
    public SequenceIndex SequenceIndex { get; }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return OrganisationName.Value;
        yield return Location;
        yield return TimePeriod;
        yield return SequenceIndex.Value;
    }
}



