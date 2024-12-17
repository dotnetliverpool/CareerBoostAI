using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class Candidate
{
    public Guid Id { get; private set; }
    private CandidateFirstName _firstName;
    private CandidateLastName _lastName;
    private CandidateDOB _dateOfBirth;
    private List<CandidateEmail> _emails = new();
    private List<PhoneNumber> _phoneNumbers = new();
    private List<CandidateCV> _cvs = new();
    

    public Candidate(
        CandidateFirstName firstName, 
        CandidateLastName lastName,
        CandidateDOB dateOfBirth)
    {
        _firstName = firstName;
        _lastName = lastName;
        _dateOfBirth = dateOfBirth;
    }

    
}