namespace CareerBoostAI.Application.Candidate.DTO;

public class ParsedCvDocumentDto
{
    public  string? Summary { get; init; } = null;
    public  IEnumerable<ParsedCvEducationDto> Educations { get; init; } = new List<ParsedCvEducationDto>();
    public  IEnumerable<ParsedCvExperienceDto> Experiences { get; init; } = new List<ParsedCvExperienceDto>();
    public  IEnumerable<string> Skills { get; init; } = new List<string>();
    public  IEnumerable<string> Languages { get; init; } = new List<string>();
}

public abstract class ParsedCvProfessionalEntryDto
{
    public  string? OrganisationName { get; init; } = null;
    public  string? City { get; init; } = null;
    public  string? Country { get; init; } = null;
    public  DateOnly? StartDate { get; init; } = null;
    public  DateOnly? EndDate { get; init; } = null;
}

public class ParsedCvExperienceDto : ParsedCvProfessionalEntryDto
{
    public string? Description { get; init; } = null;
}

public class ParsedCvEducationDto : ParsedCvProfessionalEntryDto
{
    public string? Program { get; init; } = null;
    public string? Grade { get; init; } = null;
}