using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Cv
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; }
    
    [Required]
    [Column(TypeName = "Varchar(50)")]
    public string StorageMedium { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string StorageAddress { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(4)]
    [Column(TypeName = "Varchar(4)")]
    public string  PhoneCountryCode { get; set; }
    
    
    [MaxLength(14)]
    [Column(TypeName = "Varchar(14)")]
    public string  PhoneNumber { get; set; }
    
    
    
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
    
    public Guid CandidateId { get; set; }
    public Candidate Candidate { get; set; }
}