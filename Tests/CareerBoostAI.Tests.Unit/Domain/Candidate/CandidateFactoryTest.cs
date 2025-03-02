using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using Xunit;
using Shouldly;

namespace CareerBoostAI.Tests.Unit.Domain.Candidate;

public class CandidateFactoryTest : BaseCandidateTest
{
    [Fact]
    public void Create_ShouldCreateValidCandidateAggregate()
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateOnly(1990, 5, 15);
        var email = "john.doe@example.com";
        var phoneCode = "+1";
        var phoneNumber = "1234567890";
        var cv = CreateCandidateCv();

        // Act
        var candidate = GetCandidateFactory().Create(
            id, firstName, lastName, dateOfBirth, email, phoneCode, phoneNumber,  cv);
        
        // Assert
        
    }

    [Fact]
    public void Create_ShouldThrowException_WhenCvIsNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateOnly(1990, 5, 15);
        var email = "john.doe@example.com";
        var phoneCode = "+1";
        var phoneNumber = "1234567890";
        CandidateCv candidateCv = null!; // Passing null to test exception

        // Act
        var candidate = GetCandidateFactory().Create(
            id, firstName, lastName, dateOfBirth, email, phoneCode, phoneNumber,  candidateCv);
        
        // Assert
        
    }

    [Fact]
    public void CreateCv_ShouldCreateValidCvEntity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var summary = "A summary of the candidate's experiences.";
        var experiences = new List<(Guid id, string orgName, string city, string country, DateOnly startDate, DateOnly? endDate, string description, uint index)>
        {
            (Guid.NewGuid(), "Company A", "City A", "Country A", new DateOnly(2015, 5, 1), null, "Software Developer", 1),
        };
        var educations = new List<(Guid id, string orgName, string city, string country, DateOnly startDate, DateOnly? endDate, string program, string grade, uint index)>
        {
            (Guid.NewGuid(), "University A", "City B", "Country B", new DateOnly(2012, 9, 1), new DateOnly(2016, 6, 1), "Engineering", "B", 1),
        };
        var languages = new List<string> { "English", "French" };
        var skills = new List<string> { "C#", "JavaScript" };

        // Act
        var cv = GetCandidateFactory().CreateCv(id, summary, experiences, educations, languages, skills);

        // Assert
    }

    [Theory]
    [InlineData(10)] // Below minimum age limit
    [InlineData(121)] // Above maximum age limit
    public void Create_ShouldThrowNotAcceptedWithinAgeLimitException_WhenAgeIsOutOfRange(int age)
    {
        // Arrange
        var id = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var dateOfBirth = new DateOnly(2000, 1, 1).AddYears(-age); 
        var email = "john.doe@example.com";
        var phoneCode = "+1";
        var phoneNumber = "1234567890";
        var cvFiles = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var cv = CreateCandidateCv();
        
        // Act
    }
    
    [Fact]
    public void Cv_ShouldThrowException_WhenExperienceIndexIsNotInSequence()
    {
        // Arrange
        var invalidExperiences = new List<WorkExperience>
        {
            WorkExperience.Create(Guid.NewGuid(), "Company A", "City A", "Country A", new DateOnly(2015, 1, 1), null, "Description A", 1),
            WorkExperience.Create(Guid.NewGuid(), "Company B", "City B", "Country B", new DateOnly(2016, 1, 1), null, "Description B", 3), // Invalid index (should be 2)
        };
        var validExperiences = new List<WorkExperience>
        {
            WorkExperience.Create(Guid.NewGuid(), "Company A", "City A", "Country A", new DateOnly(2015, 1, 1), null, "Description A", 1),
            WorkExperience.Create(Guid.NewGuid(), "Company B", "City B", "Country B", new DateOnly(2016, 1, 1), null, "Description B", 2),
        };

        var cv = new CandidateCv(
            CvId.New(), 
            Summary.Create("Summary"), 
            invalidExperiences, 
            new List<Education>(), 
            new List<Skill>(), 
            new List<Language>()
        );

        
    }

    [Fact]
    public void Cv_ShouldNotThrowException_WhenExperienceIndexIsInSequence()
    {
        // Arrange
        var experiences = new List<WorkExperience>
        {
            WorkExperience.Create(Guid.NewGuid(), "Company A", "City A", "Country A", new DateOnly(2015, 1, 1), null, "Description A", 1),
            WorkExperience.Create(Guid.NewGuid(), "Company B", "City B", "Country B", new DateOnly(2016, 1, 1), null, "Description B", 2),
        };

        var cv = new CandidateCv(
            CvId.New(),
            Summary.Create("Summary"),
            experiences,
            new List<Education>(),
            new List<Skill>(),
            new List<Language>()
        );

        
    }

}