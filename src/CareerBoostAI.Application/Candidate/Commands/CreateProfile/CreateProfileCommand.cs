using CareerBoostAI.Application.Common.Abstractions.Mediator;

namespace CareerBoostAI.Application.Candidate.Commands.CreateProfile;

public sealed record CreateProfileCommand(
    string FirstName,
    string LastName,
    string Email,
    DateOnly DateOfBirth,
    string PhoneCode,
    string PhoneNumber,
    CreateCvCommand CreateCvCommand
) : ICommand<Guid>;


public sealed record CreateCvCommand(
    string Summary,
    IEnumerable<CreateExperienceCommand> Experiences,
    IEnumerable<CreateEducationCommand> Educations,
    IEnumerable<string> Skills,
    IEnumerable<string> Languages);

public sealed record CreateExperienceCommand(
    string OrganisationName,
    string City,
    string Country,
    DateOnly StartDate,
    DateOnly? EndDate,
    string Description,
    uint SequenceIndex);
public  record CreateEducationCommand(
    string OrganisationName,
    string City,
    string Country,
    DateOnly StartDate,
    DateOnly? EndDate,
    uint SequenceIndex,
    string Program,
    string Grade);
