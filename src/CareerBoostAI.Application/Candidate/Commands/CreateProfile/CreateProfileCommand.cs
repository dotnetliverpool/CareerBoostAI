using CareerBoostAI.Application.Common.Abstractions.Mediator;
using MediatR;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public sealed record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneCode,
    string PhoneNumber,
    Stream CvFile,
    string CvFileName
) : ICommand;