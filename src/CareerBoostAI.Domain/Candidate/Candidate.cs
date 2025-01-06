using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Exceptions;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class Candidate : AggregateRoot<CandidateId>
{
    public CandidateId Id { get; private set; }
    private FirstName _firstName;
    private LastName _lastName;
    private DateOfBirth _dateOfBirth;
    private  List<Email> _emails = new();
    private List<PhoneNumber> _phoneNumbers = new();
    private List<Cv> _cvs = new();

    public string FullName => _firstName + " " + _lastName;
    public Email ActiveEmail
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
        FirstName firstName, 
        LastName lastName,
        DateOfBirth dateOfBirth)
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateOfBirth = dateOfBirth;
    }

    public void AddCv(Cv cv)
    {
        if (_cvs.Any(existingCv => existingCv.Id.Equals(cv.Id)))
        {
            throw new DuplicateCandidateCvException(ActiveEmail.Value, cv.Id.Value);
        }
        _cvs.Add(cv);
    }

    

    
}