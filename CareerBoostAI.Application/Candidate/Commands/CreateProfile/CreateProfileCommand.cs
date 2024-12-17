using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public sealed record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    string PhoneCode,
    string PhoneNumber
) : IRequest;