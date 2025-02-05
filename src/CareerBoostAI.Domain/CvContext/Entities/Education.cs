using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Location = CareerBoostAI.Domain.CvContext.ValueObjects.Location;
using OrganisationName = CareerBoostAI.Domain.CvContext.ValueObjects.OrganisationName;

namespace CareerBoostAI.Domain.CvContext.Entities;

public class Education : ProfessionalEntry
{
    public EducationalGrade EducationalGrade { get;  }
    
    private Education(
        EntityId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        EducationalGrade educationalGrade)
        : base(id, organisationName, location, timePeriod)
    {
        EducationalGrade = educationalGrade;
    }
    
    #pragma warning disable CS8618
    public Education() {}

    public static Education Create(
        Guid id,
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string program, string grade)
    {
        var edId = EntityId.Create(id);
        var orgName = OrganisationName.Create(organisationName);
        var location = Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var gradeDomain = EducationalGrade.Create(program, grade);
        return new(edId, orgName, location, timePeriod, gradeDomain);
    }

    
}