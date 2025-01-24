using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Cv.FactoryTests;

public class CvFactoryTest : BaseCvTest
{
    
    [Fact]
    public void Skill_Create_ShouldReturnSkill_WhenPassedValidData()
    {
        // Arrange
        var value = "C#";

        // Act
        var skill = Skill.Create(value);

        // Assert
        skill.ShouldNotBeNull();
        skill.Value.ShouldBe(value.ToLower());  // Should convert value to lowercase
    }

    [Theory]
    [InlineData(null)]  // Null value
    [InlineData("")]    // Empty value
    public void Skill_Create_ShouldThrowEmptyArgumentException_WhenInvalidDataIsPassed(string value)
    {
        // Act & Assert
        var exception = Record.Exception(() => Skill.Create(value));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
    }
    
    [Fact]
    public void Language_Create_ShouldReturnLanguage_WhenPassedValidData()
    {
        // Arrange
        var value = "English";

        // Act
        var language = Language.Create(value);

        // Assert
        language.ShouldNotBeNull();
        language.Value.ShouldBe(value.ToLower());  // Should convert value to lowercase
    }

    [Theory]
    [InlineData(null)]  // Null value
    [InlineData("")]    // Empty value
    public void Language_Create_ShouldThrowEmptyArgumentException_WhenInvalidDataIsPassed(string value)
    {
        // Act & Assert
        var exception = Record.Exception(() => Language.Create(value));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
    }
    
    [Fact]
    public void Summary_Create_ShouldReturnSummary_WhenPassedValidData()
    {
        // Arrange
        var value = "A highly motivated software developer.";

        // Act
        var summary = Summary.Create(value);

        // Assert
        summary.ShouldNotBeNull();
        summary.Value.ShouldBe(value);  // Value should be the same as the input
    }

    [Theory]
    [InlineData(null)]  // Null value
    [InlineData("")]    // Empty value
    public void Summary_Create_ShouldThrowEmptyArgumentException_WhenInvalidDataIsPassed(string value)
    {
        // Act & Assert
        var exception = Record.Exception(() => Summary.Create(value));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
    }
    
    [Fact]
    public void Static_Create_ShouldReturnCv_WhenPassedValidData()
    {
        // Arrange
        var id = EntityId.Create(Guid.NewGuid());
        var summary = Summary.Create(GetValidCvSummary());
        var candidateEmail = Email.Create("candidate@example.com");

        var experiencesData = GetValidCvExperiences(3);
        var experiences = experiencesData
            .Select(data => Experience.Create(
                Guid.NewGuid(), data.OrganisationName, data.City,
                data.Country, data.StartDate, data.EndDate, data.Description,
                data.Index)).ToArray();
        
        var educationsData = GetValidCvEducations(3);
        var educations = educationsData
            .Select(data => Education.Create(
                Guid.NewGuid(), data.OrganisationName, data.City,
                data.Country, data.StartDate, data.EndDate, 
                data.Program, data.Grade, data.Index)).ToArray();
        
        var skillsData = GetValidCvSkills(10);
        var skills = skillsData.Select(Skill.Create).ToArray();

        var languagesData = GetValidCvLanguages(3);
        var languages = languagesData.Select(Language.Create).ToArray();

        // Act
        var cv = CareerBoostAI.Domain.CvContext.Cv.Create(
            id, summary, candidateEmail, experiences, educations, skills, languages);

        // Assert
        cv.ShouldNotBeNull();
        cv.Id.ShouldBe(id);
        cv.Summary.ShouldBe(summary);
        cv.CandidateEmail.ShouldBe(candidateEmail);
        
        // Validate Experiences
        cv.Experiences.Count.ShouldBe(experiences.Length);
        foreach (var (expectedExperience, actualExperience) in experiences.Zip(
                     cv.Experiences, (expected, actual) => (expected, actual)))
        {
            expectedExperience.ShouldBe(actualExperience);
            expectedExperience.Id.ShouldBe(actualExperience.Id);
            expectedExperience.AreEqualByValue(actualExperience);
        }
        
        // Validate Educations
        cv.Educations.Count.ShouldBe(educations.Length);
        foreach (var (expectedEducation, actualEducation) in educations.Zip(
                     cv.Educations, (expected, actual) => (expected, actual)))
        {
            expectedEducation.ShouldBe(actualEducation);
            expectedEducation.Id.ShouldBe(actualEducation.Id);
            expectedEducation.AreEqualByValue(actualEducation);
        }
        
        // Validate Skills
        cv.Skills.Count.ShouldBe(skills.Length);
        cv.Skills.Select(skill => skill.Value).ShouldBe(skills.Select(skill => skill.Value));

        // Validate Languages
        cv.Languages.Count.ShouldBe(languages.Length);
        cv.Languages.Select(language => language.Value).ShouldBe(languages.Select(language => language.Value));
    }
    
    

    [Theory]
    [InlineData(new uint[] { 1, 2, 2 })] // Invalid sequence, duplicate 2
    [InlineData(new uint[] { 1, 2, 4 })] // Invalid sequence, missing 3
    [InlineData(new uint[] { 1, 1, 2 })] // Invalid sequence, 0 is not a valid index
    public void Static_Create_ShouldThrowInvalidEntrySequenceIndexException_WhenSequenceIsInvalid(uint[] indexes)
    {
        // Arrange
        var id = EntityId.Create(Guid.NewGuid());
        var summary = Summary.Create("Valid Summary");
        var candidateEmail = Email.Create("candidate@example.com");

        // Generate experiences with the sequence indexes
        var experiencesData = GetValidCvExperiences((uint)indexes.Length)
            .Select((data, index) => Experience.Create(
                Guid.NewGuid(),
                data.OrganisationName,
                data.City,
                data.Country,
                data.StartDate,
                data.EndDate,
                data.Description,
                indexes[index]
            ));

        // Generate educations with the sequence indexes
        var educationsData = GetValidCvEducations((uint)indexes.Length)
            .Select((data, index) => Education.Create(
                Guid.NewGuid(),
                data.OrganisationName,
                data.City,
                data.Country,
                data.StartDate,
                data.EndDate,
                data.Program,
                data.Grade,
                indexes[index]
            ));

        var skillsData = GetValidCvSkills(10);
        var skills = skillsData.Select(Skill.Create);

        var languagesData = GetValidCvLanguages(3);
        var languages = languagesData.Select(Language.Create);

        // Act & Assert
        var exception = Record.Exception(() => CareerBoostAI.Domain.CvContext.Cv.Create(
            id, summary, candidateEmail, experiencesData, educationsData, skills, languages));

        // Assert: The exception should be thrown if the sequence is invalid
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProfessionalEntrySequenceInvalidException>();
    }
    
    [Fact]
    public void CreateFromData_ShouldReturnValidCv_WhenPassedValidData()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = GetValidCvData();

        // Act
        var cv = factory.CreateFromData(data);
        
        // Validate
        cv.ShouldNotBeNull();
        cv.ShouldBeOfType<CareerBoostAI.Domain.CvContext.Cv>();
        cv.Id.ShouldBeOfType<EntityId>();
        cv.Summary.ShouldBeOfType<Summary>();
        cv.CandidateEmail.ShouldBeOfType<Email>();
        cv.Experiences.Count.ShouldBe(data.Experiences.Count());
        cv.Educations.Count.ShouldBe(data.Educations.Count());
        cv.Skills.Count.ShouldBe(data.Skills.Count());
        cv.Languages.Count.ShouldBe(data.Languages.Count());

    }
}