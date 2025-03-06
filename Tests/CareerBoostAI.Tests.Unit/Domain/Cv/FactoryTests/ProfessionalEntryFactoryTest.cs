using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Cv.FactoryTests;

public class ProfessionalEntryFactoryTest
{
    [Fact]
    public void Create_ShouldReturnOrganisationName_WhenPassedValidValue()
    {
        // Arrange
        var validOrganisationName = "TechCorp";

        // Act
        var organisationName = OrganisationName.Create(validOrganisationName);

        // Assert
        organisationName.ShouldNotBeNull();
        organisationName.Value.ShouldBe(validOrganisationName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Create_ShouldThrowArgumentException_WhenPassedInvalidValue(string invalidOrganisationName)
    {
        // Act & Assert
        var exception = Record.Exception(() => OrganisationName.Create(invalidOrganisationName));

        exception.ShouldBeOfType<EmptyArgumentException>();
        exception.Message.ShouldBe("OrganisationName cannot be empty");
    }
    
    [Fact]
    public void Create_ShouldReturnLocation_WhenPassedValidValues()
    {
        // Arrange
        var city = "London";
        var country = "UK";

        // Act
        var location = Location.Create(city, country);

        // Assert
        location.ShouldNotBeNull();
        location.City.ShouldBe(city);
        location.Country.ShouldBe(country);
    }

    [Theory]
    [InlineData("", "UK", "Location.City")]
    [InlineData("London", "", "Location.Country")]
    [InlineData(null, "UK", "Location.City")]
    [InlineData("London", null, "Location.Country")]
    public void Create_ShouldThrowArgumentException_WhenPassedInvalidValues(
        string city, string country, string expectedMessage)
    {
        // Act & Assert
        var exception = Record.Exception(() => Location.Create(city, country));

        exception.ShouldBeOfType<EmptyArgumentException>();
        exception.Message.ShouldContain(expectedMessage);
    }
    
    [Theory]
    [InlineData("2020-01-01", "2021-12-31", false)]  // Valid period with start and end date
    [InlineData("2020-01-01", null, true)]           // Ongoing period with start date only
    public void Create_ShouldReturnPeriod_WhenPassedValidStartDateAndEndDate(
        string startDateString, string? endDateString, bool expectedIsOngoing)
    {
        // Arrange
        var startDate = DateOnly.Parse(startDateString);
        DateOnly? endDate = endDateString is not null ? DateOnly.Parse(endDateString) : null;

        // Act
        var period = Period.Create(startDate, endDate);

        // Assert
        period.ShouldNotBeNull();
        period.StartDate.ShouldBe(startDate);
        period.EndDate.ShouldBe(endDate);
        period.IsOngoing.ShouldBe(expectedIsOngoing);
    }
    
    [Theory]
    [InlineData("2020-12-31", "2020-01-01")]
    [InlineData("2022-05-01", "2022-04-30")] // Example of another invalid case
    public void Create_ShouldThrowInvalidProfessionalEntryTimePeriodException_WhenEndDateIsBeforeStartDate(
        string startDateString, string endDateString)
    {
        // Arrange
        var startDate = DateOnly.Parse(startDateString);
        var endDate = DateOnly.Parse(endDateString);

        // Act & Assert
        var exception = Record.Exception(() => Period.Create(startDate, endDate));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidProfessionalEntryTimePeriodException>();
    }
}