namespace CareerBoostAI.Application.Candidate.DTO;

public class CvDto
{
    public required Guid Id { get; init; }
    public required string Summary { get; init; }
    public required IEnumerable<EducationDto> Educations { get; init; }
    public required IEnumerable<ExperienceDto> Experiences { get; init; }
    public required IEnumerable<string> Skills { get; init; }
    public required IEnumerable<string> Languages { get; init; }
}

public abstract class ProfessionalEntryDto
{
    public required string OrganisationName { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly? EndDate { get; init; }
    public required uint Index { get; init; }
}

public class ExperienceDto : ProfessionalEntryDto
{
    public required string Description { get; init; }
}

public class EducationDto : ProfessionalEntryDto
{
    public required string Program { get; init; }
    public required string Grade { get; init; }
}