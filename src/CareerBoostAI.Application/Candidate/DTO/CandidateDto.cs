using CareerBoostAI.Application.DTO;

namespace CareerBoostAI.Application.Candidate.DTO;

public class CandidateDto
{
    public Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required string Email { get; init; }
    public required PhoneNumberDto PhoneNumber { get; init; }
    public required CvDto Cv { get; init; }
}