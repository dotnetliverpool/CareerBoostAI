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
    private readonly List<Cv.Cv> _cvs;

    #region Public Properties
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyCollection<Cv.Cv> Cvs => _cvs.AsReadOnly();
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    

    #endregion
    
    internal CandidateAggregate(
        CandidateId id,
        FirstName firstName, LastName lastName,
        DateOfBirth dateOfBirth, Email email, 
        PhoneNumber phoneNumber, IEnumerable<Cv.Cv> cvs)
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
        var cv = new Cv.Cv(CvId.New(), file);;
        AddCv(cv);
        // TODO : Register Domain Event
    }
    public void AddCv(Cv.Cv cv)
    {
        ValidateCv(cv);
        _cvs.Add(cv);
    }

    private void ValidateCv(Cv.Cv cv)
    {
        if (Cvs.Any(existingCv => existingCv.Id.Equals(cv.Id)))
        {
            throw new DuplicatePropertyException(
                nameof(CandidateAggregate),
                nameof(Cv.Cv), cv.Id);
        }
    }
    
}