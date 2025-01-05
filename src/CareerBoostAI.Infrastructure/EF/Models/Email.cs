using System.ComponentModel.DataAnnotations;

namespace CareerBoostAI.Infrastructure.EF.Models;

public class Email
{
    public Guid Id { get; set; }
    
    [MaxLength(255)]
    public string Address { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
    
    public int OwnerId { get; set; } 
    
    [Required]
    public string OwnerType { get; set; }
}