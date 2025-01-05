using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Candidate
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateOnly DateOfBirth { get; set; }
    
    public List<Email> Emails { get; set; }
    public List<PhoneNumber> PhoneNumbers { get; set; }
    public List<Cv> Cvs { get; set; }
}