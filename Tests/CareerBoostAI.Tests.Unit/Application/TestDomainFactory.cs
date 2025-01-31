using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
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
        return GetDefaultCandidate(id: id, firstName: "john");
    }

    public CareerBoostAI.Domain.CandidateContext.Candidate GetDefaultCandidate(
        Guid? id, 
        string? firstName = null, 
        string? lastName = null, 
        DateOnly? dateOfBirth = null, 
        string? email = null, 
        string? phoneCode = null, 
        string? phoneNumber = null)
    {
        return new CareerBoostAI.Domain.CandidateContext.Candidate(
            EntityId.Create(id ?? Guid.NewGuid()),
            Name.Create(firstName ?? "John", lastName ?? "Doe"),
            DateOfBirth.Create(dateOfBirth ?? DateOnly.Parse("1998-12-01")),
            Email.Create(email ?? "johndoe@example.com"),
            PhoneNumber.Create(phoneCode ?? "+44", phoneNumber ?? "1234567890")
        );
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
                exp.StartDate, exp.EndDate, exp.Description)),
            command.Educations.Select(edu => Education.Create(
                Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                edu.StartDate, edu.EndDate, edu.Program, edu.Grade)),
            command.Skills.Select(Skill.Create), command.Languages.Select(Language.Create)
            );
    }
    
    public Cv GetCvFromCommand(Guid id, CreateCvCommand command)
    {
        
        return Cv.Create(EntityId.Create(id),
            Summary.Create(command.Summary), Email.Create("johndoe@example.com"),
            command.Experiences.Select(exp => Experience.Create(
                Guid.NewGuid(), exp.OrganisationName, exp.City, exp.Country, 
                exp.StartDate, exp.EndDate, exp.Description)),
            command.Educations.Select(edu => Education.Create(
                Guid.NewGuid(), edu.OrganisationName, edu.City, edu.Country,
                edu.StartDate, edu.EndDate, edu.Program, edu.Grade)),
            command.Skills.Select(Skill.Create), command.Languages.Select(Language.Create)
        );
    }
}