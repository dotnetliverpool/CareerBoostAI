namespace CareerBoostAI.Application.Services.EmailService;

public interface IEmailSender
{
    Task SendEmailToAdminAsync(string subject, string body, IEnumerable<string>? attachments = null);
}