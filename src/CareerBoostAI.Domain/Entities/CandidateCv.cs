using System.Collections.Specialized;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class CandidateCv
{

    public CandidateCvId CvId { get; private set; } 
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
        CandidateCvId cvId)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _phoneNumber = phoneNumber;
        _about = about;
        CvId = cvId;
        _address = address;
    }
    
    public void AddSection(CvSection section)
    {
        _sections.Add(section);
    }
}