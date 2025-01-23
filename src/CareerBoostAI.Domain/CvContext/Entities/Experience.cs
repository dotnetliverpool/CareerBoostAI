using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Description = CareerBoostAI.Domain.CvContext.ValueObjects.Description;
using Location = CareerBoostAI.Domain.CvContext.ValueObjects.Location;
using OrganisationName = CareerBoostAI.Domain.CvContext.ValueObjects.OrganisationName;
using SequenceIndex = CareerBoostAI.Domain.CvContext.ValueObjects.SequenceIndex;

namespace CareerBoostAI.Domain.CvContext.Entities;

public class Experience : ProfessionalEntry
{
    private Experience(
        EntityId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        Description description,
        SequenceIndex sequenceIndex)
        : base(id, organisationName, location, timePeriod, sequenceIndex)
    {
        Description = description;
    }
    
    public Description Description { get; }

    public static Experience Create(
        Guid id,
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string description, uint index)
    {
        var expId = EntityId.Create(id);
        var orgName = OrganisationName.Create(organisationName);
        var location = Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var descriptionDomain = Description.Create(description);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(expId, orgName, location, timePeriod, descriptionDomain, sequenceIndex);
    }
    
    
    
    
}