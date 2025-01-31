using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Description = CareerBoostAI.Domain.CvContext.ValueObjects.Description;
using Location = CareerBoostAI.Domain.CvContext.ValueObjects.Location;
using OrganisationName = CareerBoostAI.Domain.CvContext.ValueObjects.OrganisationName;

namespace CareerBoostAI.Domain.CvContext.Entities;

    public class Experience : ProfessionalEntry
    {
        private Experience(
            EntityId id,
            OrganisationName organisationName,
            Location location,
            Period timePeriod,
            Description description)
            : base(id, organisationName, location, timePeriod)
        {
            Description = description;
        }
        
        public Description Description { get; }

        public static Experience Create(
            Guid id,
            string organisationName,
            string city, string country,
            DateOnly startDate, DateOnly? endDate,
            string description)
        {
            var expId = EntityId.Create(id);
            var orgName = OrganisationName.Create(organisationName);
            var location = Location.Create(city, country);
            var timePeriod = Period.Create(startDate, endDate);
            var descriptionDomain = Description.Create(description);
            return new(expId, orgName, location, timePeriod, descriptionDomain);
        }
    
    
    
    
}