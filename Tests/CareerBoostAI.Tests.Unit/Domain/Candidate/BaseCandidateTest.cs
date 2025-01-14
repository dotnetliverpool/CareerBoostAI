using CareerBoostAI.Domain.Candidate.Cv;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;
using NSubstitute;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

internal class TestDateTimeProvider : IDateTimeProvider
{
    public DateOnly TodayAsDate => new DateOnly(2025, 1, 25);
}

public abstract class BaseCandidateTest
{
    protected CareerBoostAI.Domain.Candidate.CandidateAggregate GetNewCandidateWithCv()
    {
        IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        CandidateCv candidateCv = CreateCandidateCv();
        
        DateOnly dateOfBirth = new DateOnly(1998, 12, 13);
        return new (
            CandidateId.New(), FirstName.Create("John"), 
            LastName.Create("Doe"),
            DateOfBirth.Create(dateOfBirth, dateTimeProvider), 
            Email.Create("john.doe@gmail.com"), 
            PhoneNumber.Create("+44", "88436287893"), 
            candidateCv
             );
    }

    protected CandidateCv CreateCandidateCv()
    {
        CvId cvId = CvId.Create(Guid.NewGuid()); 
        Summary summary = Summary.Create(
            "A passionate software engineer with 5+ years of experience in web development."); 

        // Sample Experiences
        var experiences = new List<WorkExperience>
        {
            WorkExperience.Create(
                Guid.NewGuid(),
                "TechCorp", "New York", "USA", 
                new DateOnly(2018, 5, 1), 
                new DateOnly(2020, 12, 31), 
                "Backend developer, responsible for building APIs", 1),
            WorkExperience.Create(
                Guid.NewGuid(),
                "CodeWorks", "London", "UK", 
                new DateOnly(2021, 1, 1), 
                null, 
                "Lead software engineer, developing scalable web applications", 2)
        };


        var educations = new List<Education>
        {
            Education.Create(
                Guid.NewGuid(),
                "University of Tech", 
                "San Francisco", 
                "USA", 
                new DateOnly(2014, 9, 1), 
                new DateOnly(2018, 6, 1), 
                "Computer Science", "A", 1)
        };


        var skills = new List<Skill>
        {
            Skill.Create("C#"),
            Skill.Create("SQL"),
            Skill.Create("JavaScript")
        };

    
        var languages = new List<Language>
        {
            Language.Create("English"),
            Language.Create("Spanish")
        };
        
        return new CandidateCv(cvId, summary, experiences, educations, skills, languages);
    }

    protected ICandidateFactory GetCandidateFactory()
    {
        IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        return new CandidateFactory(dateTimeProvider);
    }
}