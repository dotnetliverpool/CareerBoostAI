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
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    [Required]
    [MaxLength(5)]
    public string PhoneCode { get; set; }
    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; }
    public ICollection<Upload> Uploads { get; set; }
    
    public Guid CVId { get; set; }
    public Cv Cv { get; set; }
}