namespace CareerBoostAI.Infrastructure.Services.EmailService;

public class SmtpEmailOptions
{
    public string DefaultFromAddress { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public IEnumerable<string> DefaultToAddresses { get; set; }
}

public class GoogleMailOptions
{
    public string DefaultFromAddress { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public IEnumerable<string> DefaultToAddresses { get; set; }
}