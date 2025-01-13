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
    CvData CvData
    
) : ICommand;

public sealed record CvData(
    string Summary,
    IEnumerable<Experience> Experiences,
    IEnumerable<Education> Educations,
    IEnumerable<string> Skills,
    IEnumerable<string> Languages);

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
