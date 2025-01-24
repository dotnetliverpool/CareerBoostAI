using CareerBoostAI.Domain.CvContext.Entities;
using Shouldly;

namespace CareerBoostAI.Tests.Unit.Domain.Cv;

public static class Extensions
{
    public static void AreEqualByValue(this Experience experience, Experience other)
    {
        // TODO: This exposes a design flaw
        // and entity should only be identifiable by its Id not its properties
        experience.Id.ShouldBe(other.Id);
        experience.OrganisationName.ShouldBe(other.OrganisationName);
        experience.Location.ShouldBe(other.Location);
        experience.TimePeriod.ShouldBe(other.TimePeriod);
        experience.Description.ShouldBe(other.Description);
        experience.SequenceIndex.ShouldBe(other.SequenceIndex);
    }
    
    public static void AreEqualByValue(this Education education, Education other)
    {
        // TODO: This exposes a design flaw
        // and entity shouldonly b identifiable by its Id not its properties
        education.Id.ShouldBe(other.Id);
        education.OrganisationName.ShouldBe(other.OrganisationName);
        education.Location.ShouldBe(other.Location);
        education.TimePeriod.ShouldBe(other.TimePeriod);
        education.EducationalGrade.ShouldBe(other.EducationalGrade);
        education.SequenceIndex.ShouldBe(other.SequenceIndex);
    }
}