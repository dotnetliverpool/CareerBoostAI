using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate;

public class Candidate : AggregateRoot<CandidateId>
{
    public CandidateId Id { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    
    public List<Email> Emails { get; private set; } = new();
    public List<PhoneNumber> PhoneNumbers { get; private set; } = new();
    public List<Cv.Cv> Cvs { get; private set; } = new();
    
    public string FullName => $"{FirstName.Value} {LastName.Value}";
    public Email ActiveEmail => Emails.FirstOrDefault(e => e.IsActive) 
                                ?? throw new NoActiveEmailFoundException(FullName);

    public Candidate(
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
    
    public void AddEmail(Email email)
    {
        if (Emails.Any(e => e.Equals(email)))
        {
            throw new DuplicatePropertyException(
                nameof(Candidate), 
                nameof(Email), email.Value);
        }
        Emails.Add(email);
    }

    public void AddEmails(IEnumerable<Email> emails)
    {
        foreach (var email in emails)
        {
            AddEmail(email);
        }
    }
    
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
        
        PhoneNumbers.Add(phoneNumber);
    }

    public void RegisterCv(CvFile file)
    {
        var cv = new Cv.Cv(CvId.New(), file);;
        AddCv(cv);
    }
    public void AddCv(Cv.Cv cv)
    {
        ValidateCv(cv);
        Cvs.Add(cv);
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