using CareerBoostAI.Domain.Candidate.ValueObjects;
using CareerBoostAI.Domain.Common.ValueObjects;

namespace CareerBoostAI.Domain.Candidate.Cv.ValueObjects;

public abstract class BaseCvContent
{
    public virtual void AddSection(CvSection section)
    { }
}

public sealed class NullCvContent : BaseCvContent
{
    public static readonly NullCvContent Instance = new();
}


public sealed class CvContent(
    FirstName firstName,
    LastName lastName,
    Email email,
    PhoneNumber phoneNumber,
    CvAddress address,
    CvAbout about)
    : BaseCvContent
{
    public FirstName FirstName { get; private set; } = firstName;
    public LastName LastName { get; private set; } = lastName;
    public Email Email { get; private set; } = email;
    public PhoneNumber PhoneNumber { get; private set; } = phoneNumber;
    public CvAddress Address { get; private set; } = address;
    public CvAbout About { get; private set; } = about;

    private List<CvSection> _sections = new();


    public override void AddSection(CvSection section)
    {
        if (_sections.Contains(section))
        {
            // TODO: Decide Business Logic For Duplicate Content, break or add
            return;
        }
        _sections.Add(section);
    }
}

