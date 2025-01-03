namespace CareerBoostAI.Infrastructure.EF.Models;

public class Email
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public int OwnerId { get; set; } 
    public string OwnerType { get; set; }
}