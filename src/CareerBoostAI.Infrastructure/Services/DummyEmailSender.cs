using CareerBoostAI.Application.Services.EmailService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyEmailSender : IEmailSender
{
    public Task SendEmailToAdminAsync(string subject, string body, IEnumerable<string>? attachments = null)
    {
        throw new NotImplementedException();
    }
}