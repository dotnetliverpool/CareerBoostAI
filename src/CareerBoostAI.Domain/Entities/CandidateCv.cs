using System.Collections.Specialized;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class CandidateCv
{

    public CandidateCvId Id { get; private set; }
    private CvFile _file;
    private CandidateFirstName _firstName;
    private CandidateLastName _lastName;
    private CandidateEmail _email;
    private PhoneNumber _phoneNumber;
    private CvAddress _address;
    private CandidateCvAbout _about;
    private List<CvSection> _sections = new();
    
    public CandidateCv(
        CandidateFirstName firstName, 
        CandidateLastName lastName, 
        CandidateEmail email, 
        PhoneNumber phoneNumber, 
        CvAddress address,
    CandidateCvAbout about, 
        CandidateCvId id, CvFile file)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _phoneNumber = phoneNumber;
        _about = about;
        Id = id;
        _file = file;
        _address = address;
    }
    
    public void AddSection(CvSection section)
    {
        _sections.Add(section);
    }
}