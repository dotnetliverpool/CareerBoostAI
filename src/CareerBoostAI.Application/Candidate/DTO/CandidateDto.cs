using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Candidate.DTO;

public class CandidateDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<string> Emails { get; set; } = new();
    public List<string> PhoneNumbers { get; set; } = new();
    public List<CvDto> Cvs { get; set; } = new();
}