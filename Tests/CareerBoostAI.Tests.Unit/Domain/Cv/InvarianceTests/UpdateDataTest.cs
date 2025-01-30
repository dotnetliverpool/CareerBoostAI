using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Cv.InvarianceTests;

public class ModifyContentTest : BaseCvTest
{
    public static IEnumerable<object[]> SummaryTestData =>
        new List<object[]>
        {
            new object[] { "New summary 1", "New summary 1" },
            new object[] { "Updated summary with additional details", "Updated summary with additional details" },
            new object[] { "Short summary", "Short summary" },
            new object[] { "Summary with special characters: #$%@!", "Summary with special characters: #$%@!" }
        };

    [Theory]
    [MemberData(nameof(SummaryTestData))]
    public void UpdateSummary_ShouldUpdateCvSummary_WhenValidDataIsProvided(
        string inputSummary, string expectedSummary)
    {
        // Arrange
        var factory = GetCvFactory();
        var data = GetValidCvData();

        var cv = factory.CreateFromData("candidate@cv.com", data);

        // Act
        cv.UpdateSummary(inputSummary);

        // Assert
        cv.Summary.ShouldNotBeNull();
        cv.Summary.Value.ShouldBe(expectedSummary);
    }

    [Fact]
    public void UpdateSkills_ShouldAddSkills_WhenSkillsWereEmpty()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = []
        };

        var skillsData = new[] { "C#", "ASP.NET Core" };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        // Act
        cv.UpdateSkills(skillsData);

        // Assert
        cv.Skills.Count.ShouldBe(2);
        cv.Skills.ShouldContain(Skill.Create("C#"));
        cv.Skills.ShouldContain(Skill.Create("ASP.NET Core"));
    }

    [Fact]
    public void UpdateSkills_ShouldReplaceSkills_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = ["C#", "ASP.NET Core", "PYTHON", "PHP"]
        };

        var skillsData = new[] { "C#", "SQL", "AZURE" };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        // Act
        cv.UpdateSkills(skillsData);

        // Assert
        cv.Skills.Count.ShouldBe(3);
        cv.Skills.ShouldContain(Skill.Create("C#"));
        cv.Skills.ShouldContain(Skill.Create("SQL"));
        cv.Skills.ShouldContain(Skill.Create("AZURE"));
    }

    [Fact]
    public void UpdateLanguages_ShouldAddLanguages_WhenLanguagesWereEmpty()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = [],
            Skills = GetValidCvSkills(2)
        };

        var languagesData = new[] { "English", "Spanish" };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        // Act
        cv.UpdateLanguages(languagesData);

        // Assert
        cv.Languages.Count.ShouldBe(2);
        cv.Languages.ShouldContain(Language.Create("English"));
        cv.Languages.ShouldContain(Language.Create("Spanish"));
    }

    [Fact]
    public void UpdateLanguages_ShouldReplaceLanguages_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = new List<string> { "English", "Spanish", "French", "German" },
            Skills = ["C#", "ASP.NET Core", "PYTHON", "PHP"]
        };

        var languagesData = new[] { "English", "Arabic" };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        // Act
        cv.UpdateLanguages(languagesData);

        // Assert
        cv.Languages.Count.ShouldBe(2);
        cv.Languages.ShouldContain(Language.Create("English"));
        cv.Languages.ShouldContain(Language.Create("Arabic"));
        cv.Languages.ShouldNotContain(Language.Create("Spanish"));
        cv.Languages.ShouldNotContain(Language.Create("French"));
        cv.Languages.ShouldNotContain(Language.Create("German"));
    }

    [Fact]
    public void UpdateExperiences_ShouldReplaceExperiences_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = GetValidCvSkills()
        };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        var newExperiences = new[]
        {
            new ExperienceData
            {
                OrganisationName = "New Company 1", City = "City1", Country = "Country1",
                StartDate = DateOnly.Parse("2019-03-01"), EndDate = DateOnly.Parse("2021-03-01"),
                Description = "Experience 1", Index = 1
            },
            new ExperienceData
            {
                OrganisationName = "New Company 2", City = "City2", Country = "Country2",
                StartDate = DateOnly.Parse("2016-03-01"), EndDate = DateOnly.Parse("2017-03-01"),
                Description = "Experience 2", Index = 2
            }
        };

        // Act
        cv.UpdateExperiences(newExperiences);

        // Assert
        cv.Experiences.Count.ShouldBe(2);
        cv.HasExperienceAt("New Company 1").ShouldBeTrue();
        cv.HasExperienceAt("New Company 2").ShouldBeTrue();
    }

    [Fact]
    public void UpdateExperiences_ShouldThrowException_WhenSequenceIsInvalid()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = GetValidCvSkills()
        };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        var invalidExperiences = new[]
        {
            new ExperienceData
            {
                OrganisationName = "New Company 1", City = "City1", Country = "Country1",
                StartDate = DateOnly.Parse("2019-03-01"), EndDate = DateOnly.Parse("2021-03-01"),
                Description = "Experience 1", Index = 1
            },
            new ExperienceData
            {
                OrganisationName = "New Company 2", City = "City2", Country = "Country2",
                StartDate = DateOnly.Parse("2016-03-01"), EndDate = DateOnly.Parse("2017-03-01"),
                Description = "Experience 2", Index = 5
            }
        };

        // Act
        var exception = Record.Exception(() => cv.UpdateExperiences(invalidExperiences));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProfessionalEntrySequenceInvalidException>();
    }

    [Fact]
    public void UpdateEducations_ShouldReplaceEducations_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = GetValidCvSkills()
        };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        var newEducations = new[]
        {
            new EducationData
            {
                OrganisationName = "New University 1", City = "City1", Country = "Country1",
                StartDate = DateOnly.Parse("2015-09-01"), EndDate = DateOnly.Parse("2019-06-01"),
                Program = "Program 1", Grade = "A", Index = 1
            },
            new EducationData
            {
                OrganisationName = "New University 2", City = "City2", Country = "Country2",
                StartDate = DateOnly.Parse("2019-09-01"), EndDate = DateOnly.Parse("2021-06-01"),
                Program = "Master's Degree", Grade = "C", Index = 2
            }
        };

        // Act
        cv.UpdateEducations(newEducations);

        // Assert
        cv.Educations.Count.ShouldBe(2);
        cv.HasEducationalBackgroundAt("New University 1").ShouldBeTrue();
        cv.HasEducationalBackgroundAt("New University 2").ShouldBeTrue();
    }

    [Fact]
    public void UpdateEducations_ShouldThrowException_WhenSequenceIsInvalid()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = GetValidCvSkills()
        };

        var cv = factory.CreateFromData("candidate@cv.com", data);

        var invalidEducations = new[]
        {
            new EducationData
            {
                OrganisationName = "New University 1", City = "City1", Country = "Country1",
                StartDate = DateOnly.Parse("2015-09-01"), EndDate = DateOnly.Parse("2019-06-01"),
                Program = "Program 1", Grade = "A", Index = 1
            },
            new EducationData
            {
                OrganisationName = "New University 2", City = "City2", Country = "Country2",
                StartDate = DateOnly.Parse("2019-09-01"), EndDate = DateOnly.Parse("2021-06-01"),
                Program = "Master's Degree", Grade = "C", Index = 5
            }
        };

        // Act
        var exception = Record.Exception(() => cv.UpdateEducations(invalidEducations));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ProfessionalEntrySequenceInvalidException>();
    }
}