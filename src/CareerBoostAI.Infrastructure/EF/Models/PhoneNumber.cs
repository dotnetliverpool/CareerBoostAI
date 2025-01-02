namespace CareerBoostAI.Infrastructure.EF.Models;

public class PhoneNumber
{
    public int Id { get; set; }
    public string CountryCode { get; set; } 
    public string Number { get; set; }
    public bool IsActive { get; set; }
    public int OwnerId { get; set; } 
    public string OwnerType { get; set; }
}