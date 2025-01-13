﻿using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;

public class Education : ProfessionalEntry
{
    public EducationGrade Grade { get; set; }
    
    private Education(
        OrganisationName organisationName,
        Location location,
        Period timePeriod,
        EducationGrade educationGrade,
        SequenceIndex sequenceIndex)
        : base(organisationName, location, timePeriod, sequenceIndex)
    {
        Grade = educationGrade;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Grade;
        yield return base.GetAtomicValues();
    }

    public static Education Create(
        string organisationName,
        string city, string country,
        DateOnly startDate, DateOnly? endDate,
        string program, string grade, uint index)
    {
        var orgName = OrganisationName.Create(organisationName);
        var location = ValueObjects.Location.Create(city, country);
        var timePeriod = Period.Create(startDate, endDate);
        var gradeDomain = EducationGrade.Create(program, grade);
        var sequenceIndex = SequenceIndex.Create(index);
        return new(orgName, location, timePeriod, gradeDomain, sequenceIndex);
    }

    
}