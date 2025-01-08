using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class PhoneNumber
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(4)]
    [Column(TypeName = "Varchar(4")]
    public string CountryCode { get; set; } 
    
    [Required]
    [MaxLength(14)]
    [Column(TypeName = "Varchar(14")]
    public string Number { get; set; }
    
    public bool IsActive { get; set; }
    public Guid OwnerId { get; set; } 
    public string OwnerType { get; set; }
}