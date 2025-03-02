namespace CareerBoostAI.Infrastructure.Services.EmailService;

public class EmailOptions
{
    public string DefaultFromAddress { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DefaultToAddresses { get; set; }
}

