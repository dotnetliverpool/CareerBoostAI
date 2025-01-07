using CareerBoostAI.Domain.Abstractions;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.Exceptions;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

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


    public void AddCv(Cv.Cv cv)
    {
        if (Cvs.Any(existingCv => existingCv.Id.Equals(cv.Id)))
        {
            throw new DuplicatePropertyException(
                nameof(Candidate),
                nameof(Cv.Cv), cv.Id);
        }
        Cvs.Add(cv);
    }

    

    
}