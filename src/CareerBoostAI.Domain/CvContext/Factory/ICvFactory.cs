namespace CareerBoostAI.Domain.CvContext.Factory;

public interface ICvFactory
{
    Cv CreateFromData(CvData data);
}


// TODO : Unnecessary repitition (Refactor)
public class CvData
{
    public required IEnumerable<ExperienceData> Experiences { get; init; }
    public required IEnumerable<EducationData> Educations { get; init; }
    public required IEnumerable<string> Skills { get; init; }
    public required IEnumerable<string> Languages { get; init; }
    public required string Summary { get; init; }
    public required string CandidateEmail { get; init; }
}

public abstract class ProfessionalEntryData
{
    public required string OrganisationName { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly? EndDate { get; init; }
    public required uint Index { get; init; }
}

public class ExperienceData : ProfessionalEntryData
{
    public required string Description { get; init; }
}

public class EducationData : ProfessionalEntryData
{
    public required string Program { get; init; }
    public required string Grade { get; init; }
}