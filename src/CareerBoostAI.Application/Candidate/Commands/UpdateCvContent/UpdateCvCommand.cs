using CareerBoostAI.Application.Common.Abstractions.Mediator;

namespace CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;


public sealed record UpdateCvCommand(
    string Email,
    string Summary,
    IEnumerable<Experience> Experiences,
    IEnumerable<Education> Educations,
    IEnumerable<string> Skills,
    IEnumerable<string> Languages) : ICommand;

public sealed record Experience(
    string OrganisationName,
    string City,
    string Country,
    DateOnly StartDate,
    DateOnly? EndDate,
    string Description,
    uint SequenceIndex);
public  record Education(
    string OrganisationName,
    string City,
    string Country,
    DateOnly StartDate,
    DateOnly? EndDate,
    uint SequenceIndex,
    string Program,
    string Grade);