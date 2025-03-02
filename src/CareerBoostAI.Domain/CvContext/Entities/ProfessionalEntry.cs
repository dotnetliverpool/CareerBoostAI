using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Domain.CvContext.Entities;

public abstract class ProfessionalEntry : Entity<EntityId>
{
    protected ProfessionalEntry(
        EntityId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod)
    {
        Id = id;
        OrganisationName = organisationName;
        Location = location;
        TimePeriod = timePeriod;
    }
    
    #pragma warning disable CS8618
    protected ProfessionalEntry() {}

    public OrganisationName OrganisationName { get; }
    public Location Location { get; }
    public Period TimePeriod { get; }
    
}



