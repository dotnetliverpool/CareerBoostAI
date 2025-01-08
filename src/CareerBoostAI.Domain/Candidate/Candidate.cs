using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Events;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate;

public class Candidate : AggregateRoot<CandidateId>
{
    
    private readonly List<Email> _emails = new();
    private readonly List<PhoneNumber> _phoneNumbers = new();
    private readonly List<Cv.Cv> _cvs = new();

    #region Public Properties

    public CandidateId Id { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public IReadOnlyCollection<Email> Emails => _emails.AsReadOnly();
    public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();
    public IReadOnlyCollection<Cv.Cv> Cvs => _cvs.AsReadOnly();
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    private Email? ActiveEmail 
        => Emails.FirstOrDefault(e => e.IsActive) 
           ?? null;

    #endregion
    

    internal Candidate(
        CandidateId id,
        FirstName firstName, 
        LastName lastName,
        DateOfBirth dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    #region CandidateEmailBehaviour

    public void RegisterEmail(Email email)
    {
        // email is active
        if (!email.IsActive)
        {
            throw new AttemptToRegisterInactiveEmailException(Id.Value, email.Value );
        }
        
        // email is not duplicated
        ValidateEmail(email);
        
        var activeEmail = ActiveEmail;
        if (activeEmail is not null)
        {
            _emails.Remove(activeEmail);
            var inactiveEmail = Email.Create(activeEmail.Value, isActive: false);
            _emails.Add(inactiveEmail);
        }
        _emails.Add(email);
        
        AddEvent(new EmailRegisteredEvent(Id.Value, email.Value));
    }

    private void ValidateEmail(Email email)
    {
        if (_emails.Any(e => e.Equals(email)))
        {
            throw new DuplicatePropertyException(
                nameof(Candidate), 
                nameof(Email), email.Value);
        }
    }

    internal void AddEmail(Email email)
    {
        ValidateEmail(email);
        _emails.Add(email);
    }

    internal void AddEmails(IEnumerable<Email> emails)
    {
        foreach (var email in emails)
        {
            AddEmail(email);
        }
    }

    #endregion

    #region PhoneNumberBehaviour

    public void AddPhoneNumbers(IEnumerable<PhoneNumber> numbers)
    {
        foreach (var numner in numbers)
        {
            AddPhoneNumber(numner);
        }
    }
    
    public void AddPhoneNumber(PhoneNumber phoneNumber)
    {
        if (PhoneNumbers.Any(p => p.Equals(phoneNumber)))
        {
            throw new DuplicatePropertyException(
                nameof(Candidate),
                nameof(PhoneNumber), phoneNumber.ToString());
        }
        
        _phoneNumbers.Add(phoneNumber);
    }

    #endregion
    
    

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
                nameof(Candidate),
                nameof(Cv.Cv), cv.Id);
        }
    }

    public void AddCvs(IEnumerable<Cv.Cv> cvs)
    {
        foreach (var cv in cvs)
        {
            AddCv(cv);
        }
    }

    

    
}