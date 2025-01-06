using System.Collections.Specialized;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Entities;

public class Cv
{

    public CandidateCvId Id { get; private set; }
    private CvFile _file;
    private FirstName _firstName;
    private LastName _lastName;
    private Email _email;
    private PhoneNumber _phoneNumber;
    private CvAddress _address;
    private CandidateCvAbout _about;
    private List<CvSection> _sections = new();
    
    public Cv(
        FirstName firstName, 
        LastName lastName, 
        Email email, 
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