using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity;

public class WorkExperience : ProfessionalEntry
{
    private WorkExperience(
        ProfessionalEntryId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        Description description,
        SequenceIndex sequenceIndex)
        : base(id, organisationName, location, timePeriod, sequenceIndex)
    {
        Description = description;
    }
    
    public WorkExperience() {}
    
    public Description Description { get; }

    public static WorkExperience Create(
        Guid id,
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string description, uint index)
    {
        var expId = ProfessionalEntryId.Create(id);
        var orgName = OrganisationName.Create(organisationName);
        var location = ValueObjects.Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var descriptionDomain = ValueObjects.Description.Create(description);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(expId, orgName, location, timePeriod, descriptionDomain, sequenceIndex);
    }
    
    
    
    
}