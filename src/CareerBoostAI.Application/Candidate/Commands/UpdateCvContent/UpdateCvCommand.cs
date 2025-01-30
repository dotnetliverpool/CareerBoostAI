using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Common.Abstractions.Mediator;

namespace CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;


public sealed record UpdateCvCommand(
    string Email,
    string Summary,
    IEnumerable<CreateExperienceCommand> Experiences,
    IEnumerable<CreateEducationCommand> Educations,
    IEnumerable<string> Skills,
    IEnumerable<string> Languages) : ICommand;
    