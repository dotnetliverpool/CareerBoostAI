using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using CareerBoostAI.Domain.CandidateContext.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.CvContext;
using CareerBoostAI.Domain.CvContext.Entities;
using CareerBoostAI.Domain.CvContext.ValueObjects;

namespace CareerBoostAI.Tests.Unit.Application;

public class TestDomainFactory
{
    public CareerBoostAI.Domain.CandidateContext.Candidate GetDefaultCandidate(Guid id)
    {
        return new CareerBoostAI.Domain.CandidateContext.Candidate(
            EntityId.Create(id), Name.Create("John", "Doe"), DateOfBirth.Create(DateOnly.Parse("1998-12-01")), 
            Email.Create("johndoe@example.com"), PhoneNumber.Create("+44", "1234567890"));
    }
    
    public CareerBoostAI.Domain.CandidateContext.Candidate GetCandidateFromCommand(Guid id, CreateOrUpdateProfileCommand command)
    {
        return new CareerBoostAI.Domain.CandidateContext.Candidate(
            EntityId.Create(id), Name.Create(command.FirstName, command.LastName), DateOfBirth.Create(command.DateOfBirth), 
            Email.Create(command.Email), PhoneNumber.Create(command.PhoneCode, command.PhoneNumber));
    }

    public Cv GetDefaultCv(Guid id)
    {
        var command = new CommandFactory().GetValidCreateCvCommand().AsDomainCvData();
        return Cv.Create(EntityId.Create(id),
            Summary.Create(command.Summary), Email.Create("johndoe@example.com"),
            command.Experiences.Select(exp => Experience.Create(
                Guid.NewGuid(), exp.OrganisationName, exp.City, exp.Country, 
                exp.StartDate, exp.EndDate, exp.Description, exp.Index)),
            command.Educations.Select(edu => Education.Create(
                Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                edu.StartDate, edu.EndDate, edu.Program, edu.Grade, edu.Index)),
            command.Skills.Select(Skill.Create), command.Languages.Select(Language.Create)
            );
    }
    
    public Cv GetCvFromCommand(Guid id, CreateCvCommand command)
    {
        
        return Cv.Create(EntityId.Create(id),
            Summary.Create(command.Summary), Email.Create("johndoe@example.com"),
            command.Experiences.Select(exp => Experience.Create(
                Guid.NewGuid(), exp.OrganisationName, exp.City, exp.Country, 
                exp.StartDate, exp.EndDate, exp.Description, exp.SequenceIndex)),
            command.Educations.Select(edu => Education.Create(
                Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                edu.StartDate, edu.EndDate, edu.Program, edu.Grade, edu.SequenceIndex)),
            command.Skills.Select(Skill.Create), command.Languages.Select(Language.Create)
        );
    }
}