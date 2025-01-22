using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Location = CareerBoostAI.Domain.CvContext.ValueObjects.Location;
using OrganisationName = CareerBoostAI.Domain.CvContext.ValueObjects.OrganisationName;
using SequenceIndex = CareerBoostAI.Domain.CvContext.ValueObjects.SequenceIndex;

namespace CareerBoostAI.Domain.CvContext.Entities;

public class Education : ProfessionalEntry
{
    public ProgramResult Grade { get;  }
    
    private Education(
        EntityId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        ProgramResult educationGrade,
        SequenceIndex sequenceIndex)
        : base(id, organisationName, location, timePeriod, sequenceIndex)
    {
        Grade = educationGrade;
    }

   

    public static Education Create(
        Guid id,
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string program, string grade, uint index)
    {
        var edId = EntityId.Create(id);
        var orgName = OrganisationName.Create(organisationName);
        var location = Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var gradeDomain = ProgramResult.Create(program, grade);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(edId, orgName, location, timePeriod, gradeDomain, sequenceIndex);
    }

    
}