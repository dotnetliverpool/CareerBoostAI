using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Candidate.DTO;

public class CandidateDto
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly DateOfBirth { get; set; }
    public required string Email { get; set; }
    public required PhoneNumberDto PhoneNumber { get; set; }
    public List<CvDto> Cvs { get; set; } = new();
}