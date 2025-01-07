using CareerBoostAI.Domain.Candidate.Cv.ValueObjects;
using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;
using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv;

public class Cv
{
    public CvId Id { get; private set; }
    public CvFile File { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public CvAddress Address { get; private set; }
    public CvAbout About { get; private set; }

    private List<CvSection> _sections = new();
    public IReadOnlyList<CvSection> Sections => _sections.AsReadOnly();

    public Cv(
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        PhoneNumber phoneNumber, 
        CvAddress address, 
        CvAbout about, 
        CvId id, 
        CvFile file)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        About = about;
        Id = id;
        File = file;
    }
    
    public void AddSection(CvSection section)
    {
        if (_sections.Contains(section))
        {
            // TODO: Decide Business Logic For Duplicate Content, break or add
            return;
        }
        _sections.Add(section);
    }
}