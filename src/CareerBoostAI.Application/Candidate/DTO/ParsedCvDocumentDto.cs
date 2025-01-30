namespace CareerBoostAI.Application.Candidate.DTO;

public class ParsedCvDocumentDto
{
    public  string? Summary { get; init; }
    public  IEnumerable<ParsedCvEducationDto> Educations { get; init; }
    public  IEnumerable<ParsedCvExperienceDto> Experiences { get; init; }
    public  IEnumerable<string> Skills { get; init; }
    public  IEnumerable<string> Languages { get; init; }
}

public abstract class ParsedCvProfessionalEntryDto
{
    public  string? OrganisationName { get; init; }
    public  string? City { get; init; }
    public  string? Country { get; init; }
    public  DateOnly? StartDate { get; init; }
    public  DateOnly? EndDate { get; init; }
}

public class ParsedCvExperienceDto : ParsedCvProfessionalEntryDto
{
    public string? Description { get; init; }
}

public class ParsedCvEducationDto : ParsedCvProfessionalEntryDto
{
    public string? Program { get; init; }
    public string? Grade { get; init; }
}