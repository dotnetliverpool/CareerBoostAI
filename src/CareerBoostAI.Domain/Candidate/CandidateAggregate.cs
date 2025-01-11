using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Abstractions;
using CareerBoostAI.Domain.Common.Events;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate;

public class CandidateAggregate : AggregateRoot<CandidateId>
{
    private readonly List<CvEntity.Cv> _cvs;

    #region Public Properties
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyCollection<CvEntity.Cv> Cvs => _cvs.AsReadOnly();
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    

    #endregion
    
    internal CandidateAggregate(
        CandidateId id,
        FirstName firstName, LastName lastName,
        DateOfBirth dateOfBirth, Email email, 
        PhoneNumber phoneNumber, IEnumerable<CvEntity.Cv> cvs)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        _cvs = cvs.ToList();
    }
    
    public void RegisterCv(CvFile file)
    {
        var cv = new CvEntity.Cv(CvId.New(), file);;
        AddCv(cv);
        // TODO : Register Domain Event
    }
    public void AddCv(CvEntity.Cv cv)
    {
        ValidateCv(cv);
        _cvs.Add(cv);
    }

    private void ValidateCv(CvEntity.Cv cv)
    {
    }
    
}