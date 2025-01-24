using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public class ExperienceFactoryTest : BaseCvTest
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
}