

using CareerBoostAI.Domain.ValueObjects;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Cv
{
    public int Id { get; set; }
    
    public PhoneNumber PhoneNumber { get; set; }
    public Email Email { get; set; }
    public string Country { get; set; } 
    public string State { get; set; }
    public string City { get; set; } 
    public string PostalCode { get; set; } 
    public string Address { get; set; } 
    public string About { get; set; }
    public List<CvSection> Sections { get; set; }
    
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
}