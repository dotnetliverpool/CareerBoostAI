using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class Candidate : AggregateRoot<CandidateId>
{
    public CandidateId Id { get; private set; }
    private CandidateFirstName _firstName;
    private CandidateLastName _lastName;
    private CandidateDOB _dateOfBirth;
    private List<CandidateEmail> _emails = new();
    private List<PhoneNumber> _phoneNumbers = new();
    private List<CandidateCv> _cvs = new();

    public string FullName => _firstName + " " + _lastName;
    public CandidateEmail ActiveEmail
    {
        get
        {
            var activeEmail = _emails.FirstOrDefault(email => email.IsActive);

            if (activeEmail == null)
            {
                throw new NoActiveEmailFoundException(FullName);
            }

            return activeEmail;
        }
    }

    public Candidate(
        CandidateId id,
        CandidateFirstName firstName, 
        CandidateLastName lastName,
        CandidateDOB dateOfBirth)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateOfBirth = dateOfBirth;
    }

    public void AddCv(CandidateCv cv)
    {
        if (_cvs.Any(existingCv => existingCv.Id.Equals(cv.Id)))
        {
            throw new DuplicateCandidateCvException(ActiveEmail.Value, cv.Id.Value);
        }
        _cvs.Add(cv);
    }

    

    
}