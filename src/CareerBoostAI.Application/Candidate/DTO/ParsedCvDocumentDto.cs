namespace CareerBoostAI.Application.Candidate.DTO;

public class ParsedCvDocumentDto
{
    public required string Summary { get; init; }
    public required IEnumerable<ParsedCvEducationDto> Educations { get; init; }
    public required IEnumerable<ParsedCvExperienceDto> Experiences { get; init; }
    public required IEnumerable<string> Skills { get; init; }
    public required IEnumerable<string> Languages { get; init; }
}

public abstract class ParsedCvProfessionalEntryDto
{
    public required string OrganisationName { get; init; }
    public required string City { get; init; }
    public required string Country { get; init; }
    public required DateOnly StartDate { get; init; }
    public required DateOnly? EndDate { get; init; }
    public required uint Index { get; init; }
}

public class ParsedCvExperienceDto : ParsedCvProfessionalEntryDto
{
    public required string Description { get; init; }
}

public class ParsedCvEducationDto : ParsedCvProfessionalEntryDto
{
    public required string Program { get; init; }
    public required string Grade { get; init; }
}