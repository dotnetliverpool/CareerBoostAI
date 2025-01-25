using CareerBoostAI.Domain.Common.ValueObjects;
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
    public void UpdateSummary_ShouldUpdateCvSummary_WhenValidDataIsProvided(string inputSummary, string expectedSummary)
    {
        // Arrange
        var factory = GetCvFactory();
        var data = GetValidCvData();

        var cv = factory.CreateFromData(data);

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
            CandidateEmail = "candidate@cv.com",
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = []
        };

        var skillsData = new [] {"C#", "ASP.NET Core"}; 

        var cv = factory.CreateFromData(data); 

        // Act
        cv.UpdateSkills(skillsData); // Call the UpdateSkills method with new skills

        // Assert
        cv.Skills.Count.ShouldBe(2); 
        cv.Skills.ShouldContain(Skill.Create("C#"));
        cv.Skills.ShouldContain(Skill.Create("ASP.NET Core"));
    }
    
    [Fact]
    public void UpdateSkills_ShouldIntersectWithExistingSkills_WhenSkillsAreAdded()
    {
        // Arrange
        var factory = GetCvFactory();
        var data = new CvData
        {
            Summary = GetValidCvSummary(),
            CandidateEmail = "candidate@cv.com",
            Educations = GetValidCvEducations(),
            Experiences = GetValidCvExperiences(),
            Languages = GetValidCvLanguages(),
            Skills = ["C#", "ASP.NET Core"]
        };

        var skillsData = new [] {"C#", "ASP.NET Core", "SQL"}; // New skills, with overlap (one common skill)

        var cv = factory.CreateFromData(data); 

        // Act
        cv.UpdateSkills(skillsData); 

        // Assert
        cv.Skills.Count.ShouldBe(3); // Assert that the total number of skills is 3
        cv.Skills.ShouldContain(Skill.Create("C#")); // Existing skill
        cv.Skills.ShouldContain(Skill.Create("ASP.NET Core")); // Existing skill
        cv.Skills.ShouldContain(Skill.Create("SQL")); // New skill
    }
    


}