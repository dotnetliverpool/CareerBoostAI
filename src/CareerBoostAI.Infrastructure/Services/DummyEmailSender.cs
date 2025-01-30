using System.Text.RegularExpressions;
using CareerBoostAI.Application.Services.EmailService;

namespace CareerBoostAI.Infrastructure.Services;

public class DummyEmailSender : IEmailSender
{
    public Task SendToAdminAsync(IApplicationNotification applicationNotification)
    {
        var subject = BuildSubjectFromNotification(applicationNotification);
        return Task.CompletedTask;
    }

    private string BuildSubjectFromNotification(IApplicationNotification applicationNotification)
    {
        var className = applicationNotification.GetType().Name;
        return Regex.Replace(className, "([a-z])([A-Z])", "$1 $2");
    }
}