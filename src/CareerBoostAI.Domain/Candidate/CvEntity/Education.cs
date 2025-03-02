using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity;

public class Education : ProfessionalEntry
{
    public EducationGrade Grade { get;  }
    
    private Education(
        ProfessionalEntryId id,
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        EducationGrade educationGrade,
        SequenceIndex sequenceIndex)
        : base(id, organisationName, location, timePeriod, sequenceIndex)
    {
        Grade = educationGrade;
    }
    
    public Education() {}

   

    public static Education Create(
        Guid id,
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string program, string grade, uint index)
    {
        var edId = ProfessionalEntryId.Create(id);
        var orgName = OrganisationName.Create(organisationName);
        var location = ValueObjects.Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var gradeDomain = EducationGrade.Create(program, grade);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(edId, orgName, location, timePeriod, gradeDomain, sequenceIndex);
    }

    
}