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
    public void Skill_Create_ShouldThrowArgumentNullException_WhenInvalidDataIsPassed(string value)
    {
        // Act & Assert
        var exception = Record.Exception(() => Skill.Create(value));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ArgumentNullException>();
    }
    [Fact]
    public void CreateFromData_ShouldReturnValidCv_WhenPassedValidData()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = GetValidCvData();

        // Act
        var cv = factory.CreateFromData(data);

        
    }
}