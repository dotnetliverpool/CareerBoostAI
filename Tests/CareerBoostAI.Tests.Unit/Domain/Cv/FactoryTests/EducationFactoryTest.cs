using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Cv.FactoryTests;

public class EducationFactoryTest 
{
    [Theory]
    [InlineData("BSc Computer Science", "First Class")]
    [InlineData("High School Certificate", "A+")]
    [InlineData("Graphics Design Diploma", "Pass")]
    public void Create_ShouldReturnEducationalGrade_WhenPassedValidData(
        string program, string grade)
    {

        // Act
        var educationalGrade = EducationalGrade.Create(program, grade);

        // Assert
        educationalGrade.ShouldNotBeNull();
        educationalGrade.Program.ShouldBe(program);
        educationalGrade.Grade.ShouldBe(grade);
    }

    [Theory]
    [InlineData("", "First Class")]
    [InlineData("BSc Computer Science", "")]
    [InlineData("", "")]
    public void Create_ShouldThrowArgumentNullException_WhenProgramOrGradeIsNullOrEmpty(string program, string grade)
    {
        // Act & Assert
        var exception = Record.Exception(() => EducationalGrade.Create(program, grade));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyArgumentException>();
    }
    
    [Fact]
    public void Create_ShouldReturnEducation_WhenPassedValidData()
    {
        // Arrange
        var id = Guid.NewGuid();
        var organisationName = "University of Example";
        var city = "Cambridge";
        var country = "UK";
        var startDate = new DateOnly(2014, 9, 1);
        var endDate = new DateOnly(2018, 5, 31);
        var program = "BSc Computer Science";
        var grade = "First Class";

        // Act
        var education = Education.Create(
            id, organisationName, city, country, startDate, endDate, program, grade);

        // Assert
        education.ShouldNotBeNull();
        education.Id.ShouldBe(EntityId.Create(id));
        education.Id.Value.ShouldBe(id);
        education.OrganisationName.Value.ShouldBe(organisationName);
        education.Location.City.ShouldBe(city);
        education.Location.Country.ShouldBe(country);
        education.TimePeriod.StartDate.ShouldBe(startDate);
        education.TimePeriod.EndDate.ShouldBe(endDate);
        education.EducationalGrade.Program.ShouldBe(program);
        education.EducationalGrade.Grade.ShouldBe(grade);
    }
}