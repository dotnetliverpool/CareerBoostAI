

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Candidate
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<Email> Emails { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
    public List<Cv> Cvs { get; set; }
}