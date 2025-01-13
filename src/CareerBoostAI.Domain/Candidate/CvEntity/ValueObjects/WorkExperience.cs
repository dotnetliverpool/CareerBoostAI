using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class WorkExperience : ProfessionalEntry
{
    private WorkExperience(
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        Description description,
        SequenceIndex sequenceIndex)
        : base(organisationName, location, timePeriod, sequenceIndex)
    {
        Description = description;
    }
    
    public Description Description { get; }

    public static WorkExperience Create(
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string description, uint index)
    {
        var orgName = OrganisationName.Create(organisationName);
        var location = ValueObjects.Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var descriptionDomain = ValueObjects.Description.Create(description);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(orgName, location, timePeriod, descriptionDomain, sequenceIndex);
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return base.GetAtomicValues();
        yield return Description.Value;
    }
}