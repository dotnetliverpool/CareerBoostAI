using CareerBoostAI.Domain.Common.ValueObjects;
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
}