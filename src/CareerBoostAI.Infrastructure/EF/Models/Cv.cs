using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Cv
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(4)]
    [Column(TypeName = "Varchar(4")]
    public string  PhoneCountryCode { get; set; }
    
    [Required]
    [MaxLength(14)]
    [Column(TypeName = "Varchar(14")]
    public int  PhoneNumber { get; set; }
    
    [MaxLength(255)]
    public string EmailAddress { get; set; }
    
    [MaxLength(100)]
    public string Country { get; set; } 
    
    [MaxLength(100)]
    public string City { get; set; } 
    
    [MaxLength(20)]
    public string PostalCode { get; set; } 
    
    [MaxLength(255)]
    public string Address { get; set; } 
    
    [MaxLength(1000)]
    public string About { get; set; }
    
    public List<CvSection> Sections { get; set; }
    
    public int CandidateId { get; set; }
    public Candidate Candidate { get; set; }
}