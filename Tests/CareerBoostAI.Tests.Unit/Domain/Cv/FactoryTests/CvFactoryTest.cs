using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using Shouldly;
using Xunit;
using DateOnly = System.DateOnly;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public class CvFactoryTest : BaseCvTest
{
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