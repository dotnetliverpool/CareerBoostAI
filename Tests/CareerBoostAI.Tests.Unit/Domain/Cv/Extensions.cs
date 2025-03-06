using CareerBoostAI.Domain.CvContext.Entities;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public static class Extensions
{
    public static void AreEqualByValue(this Experience experience, Experience other)
    {
        experience.Id.ShouldBe(other.Id);
        experience.OrganisationName.ShouldBe(other.OrganisationName);
        experience.Location.ShouldBe(other.Location);
        experience.TimePeriod.ShouldBe(other.TimePeriod);
        experience.Description.ShouldBe(other.Description);
    }
    
    public static void AreEqualByValue(this Education education, Education other)
    {
        education.Id.ShouldBe(other.Id);
        education.OrganisationName.ShouldBe(other.OrganisationName);
        education.Location.ShouldBe(other.Location);
        education.TimePeriod.ShouldBe(other.TimePeriod);
        education.EducationalGrade.ShouldBe(other.EducationalGrade);
    }
}