using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Cv.FactoryTests;

public class ExperienceFactoryTest
{
    [Theory]
    [InlineData("A software developer with 5+ years of experience.")]
    [InlineData("Proficient in C#, .NET, and cloud technologies.")]
    public void Description_Create_ShouldReturnDescription_WhenPassedValidValue(string validValue)
    {
        // Act
        var description = Description.Create(validValue);

        // Assert
        description.ShouldNotBeNull();
        description.Value.ShouldBe(validValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Description_Create_ShouldThrowEmptyArgumentException_WhenValueIsNullOrEmpty(string invalidValue)
    {
        // Act & Assert
        var exception = Record.Exception(() => Description.Create(invalidValue));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
    }
    
    [Fact]
    public void Experience_Create_ShouldReturnExperience_WhenPassedValidData()
    {
        // Arrange
        var id = Guid.NewGuid();
        var organisationName = "TechCorp";
        var city = "London";
        var country = "UK";
        var startDate = new DateOnly(2020, 1, 1);
        var endDate = new DateOnly(2022, 12, 31);
        var description = "Developed enterprise-level applications.";
        uint index = 1;

        // Act
        var experience = Experience.Create(
            id, organisationName, city, country, startDate, endDate, description, index);

        // Assert
        experience.ShouldNotBeNull();
        experience.Id.ShouldNotBeNull();
        experience.Id.ShouldBeOfType<EntityId>();
        experience.OrganisationName.Value.ShouldBe(organisationName);
        experience.Location.City.ShouldBe(city);
        experience.Location.Country.ShouldBe(country);
        experience.TimePeriod.StartDate.ShouldBe(startDate);
        experience.TimePeriod.EndDate.ShouldBe(endDate);
        experience.Description.Value.ShouldBe(description);
        experience.SequenceIndex.Value.ShouldBe((uint)index);
    }
}