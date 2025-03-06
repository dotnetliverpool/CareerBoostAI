using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public class CvUpdateServiceTest : BaseCvTest
{
    
    [Fact]
    public void Update_ShouldUpdateCv_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var sut = new CvUpdateService();
        var cvData = new CvData
        {
            Summary = GetValidCvSummary(),
            Experiences = GetValidCvExperiences(10).Take(5),
            Educations = GetValidCvEducations(4).Take(2),
            Skills = ["TypeScript", "Python", "Java",],
            Languages = ["Portuguese", "Italian", "Russian",]
        };

        var expUpdate = new List<ExperienceData>
        {
            new ExperienceData
            {
                OrganisationName = "Company 1",
                City = "City 1",
                Country = "Country 1",
                StartDate = DateOnly.Parse("2015-01-01"),
                EndDate = DateOnly.Parse("2017-01-01"),
                Description = "Worked as a Developer"
            },
            new ExperienceData
            {
                OrganisationName = "Company 2",
                City = "City 2",
                Country = "Country 2",
                StartDate = DateOnly.Parse("2017-01-01"),
                EndDate = DateOnly.Parse("2020-01-01"),
                Description = "Worked as a Senior Developer"
            }
        };
        var eduUpdate = new List<EducationData>
        {
            new EducationData
            {
                OrganisationName = "University 1",
                Program = "Bachelor's Degree",
                Grade = "A",
                City = "City 1",
                Country = "Country 1",
                StartDate = DateOnly.Parse("2011-09-01"),
                EndDate = DateOnly.Parse("2014-06-01")
            },
            new EducationData
            {
                OrganisationName = "University 2",
                Program = "Master's Degree",
                Grade = "A",
                City = "City 2",
                Country = "Country 2",
                StartDate = DateOnly.Parse("2014-09-01"),
                EndDate = DateOnly.Parse("2016-06-01")
            }
        };
        
        var updateData = new CvData
        {
            Summary = "An updated Summary",
            Experiences = expUpdate,
            Educations = eduUpdate,
            Skills = ["CI/CD Pipelines", "Agile Methodologies",
                "Cloud Security", "Machine Learning", "DevOps", "TDD"],
            Languages = ["English"]
        };
        
        var cv = factory.CreateFromData("candidate@cv.com", cvData);
        
        // Act
        
        sut.Update(cv, updateData);
        
        // Assert
        cv.Experiences.Count.ShouldBe(2);
        cv.Educations.Count.ShouldBe(2);
        cv.Skills.Count.ShouldBe(6);
        cv.Languages.Count.ShouldBe(1);
        
        cv.Summary.ShouldBe(Summary.Create("An updated Summary"));
    }
}