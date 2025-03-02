using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.CvEntity;
using CareerBoostAI.Domain.Candidate.CvEntity.ValueObjects;
using CareerBoostAI.Domain.Candidate.Services;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Factories;

public sealed class CandidateFactory(IDateTimeProvider dateTimeProvider) : ICandidateFactory
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    
   

    public CandidateAggregate Create(Guid id, string firstName, string lastName, DateOnly dateOfBirth, string email,
        string phoneCode, string phoneNumber,  CandidateCv candidateCv)
    {
        
        var candidateId = CandidateId.Create(id);
        var domainFirstName = FirstName.Create(firstName);
        var domainLastName = LastName.Create(lastName);
        var domainEmail = Email.Create(email);
        var domainDateOfBirth = DateOfBirth.Create(dateOfBirth, _dateTimeProvider);
        var domainPhone = PhoneNumber.Create(phoneCode, phoneNumber);
        candidateCv.ThrowIfNull();
        
        return new(
            candidateId, domainFirstName, 
            domainLastName, domainDateOfBirth,
            domainEmail, domainPhone, candidateCv);
    }

    public CandidateCv CreateCv(
        Guid id, string summary, 
        IEnumerable<(Guid id, string orgName, string city, string country, DateOnly startDate, 
            DateOnly? endDate, string description, uint index)> experiences, 
        IEnumerable<(Guid id, string orgName, string city, string country, DateOnly startDate, DateOnly? endDate, 
            string program, string grade, uint index)> educations, 
        IEnumerable<string> languages,
        IEnumerable<string> skills)
    {
        var cvId = CvId.Create(id);
        var domainSummary = Summary.Create(summary);
        var domainExperiences = experiences
            .Select(exp => WorkExperience.Create(
                exp.id,
                exp.orgName, 
                exp.city, 
                exp.country, 
                exp.startDate, 
                exp.endDate, exp.description,
                exp.index));
        var domainEducations = educations
            .Select(edu =>
                Education.Create(edu.id, edu.orgName,
                    edu.city,
                    edu.country,
                    edu.startDate,
                    edu.endDate,
                    edu.program,
                    edu.grade,
                    edu.index
                    ));
        var domainSkills = skills
            .Select(sk => Skill.Create(sk));
        var domainLanguages = languages
            .Select(lng => Language.Create(lng));
        
        return new(cvId, domainSummary, domainExperiences, 
            domainEducations, domainSkills, domainLanguages);
    }
   
}