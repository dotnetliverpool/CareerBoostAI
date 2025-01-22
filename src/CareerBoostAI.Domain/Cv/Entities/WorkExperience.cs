using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

namespace CareerBoostAI.Domain.Cv.Entities;

public class WorkExperience : Candidate.CvEntity.ProfessionalEntry
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
        var location = Candidate.CvEntity.ValueObjects.Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var descriptionDomain = Candidate.CvEntity.ValueObjects.Description.Create(description);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(expId, orgName, location, timePeriod, descriptionDomain, sequenceIndex);
    }
    
    
    
    
}