using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.ValueObjects;
using CareerBoostAI.Domain.Services;
using Shouldly;
using Xunit;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public class CvInformationUpdateServiceTest : BaseCvTest
{
    
    
    
    [Fact]
    public void Update_ShouldUpdateCv_WhenValidDataIsProvided()
    {
        // Arrange
        var factory = GetCvFactory();
        var cvData = new CvData
        {
            Summary = GetValidCvSummary(),
            CandidateEmail = "candidate@cv.com",
            Experiences = GetValidCvExperiences(10).Take(5),
            Educations = GetValidCvEducations(4).Take(2),
            Skills = GetValidCvSkills(20).Take(6),
            Languages = GetValidCvLanguages(10).Take(4)
        };
        
        var updateData = new CvData
        {
            Summary = "An Updated Summary",
            CandidateEmail = "candidate@cv.com",
            Experiences = GetValidCvExperiences(10).Skip(5),
            Educations = GetValidCvEducations(4).Skip(2),
            Skills = GetValidCvSkills(12).Skip(4),
            Languages = GetValidCvLanguages(6).Skip(3)
        };
        
        var cv = factory.CreateFromData(cvData);
        
        // Act
        
        CvInformationUpdateService.Update(cv, updateData);
        
        // Assert
        cv.Experiences.Count.ShouldBe(updateData.Experiences.Count());
        cv.Educations.Count.ShouldBe(updateData.Educations.Count());
        
        // check that duplicate skills and languages are not available i.e they are intersect of new and old
        
        // check that email is unchanged
        
        cv.Summary.ShouldBe(Summary.Create("An updated Summary"));
    }
}