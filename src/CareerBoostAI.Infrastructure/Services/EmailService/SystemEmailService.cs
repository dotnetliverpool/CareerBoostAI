using System.Text.RegularExpressions;
using CareerBoostAI.Application.Services.EmailService;
using FluentEmail.Core;

namespace CareerBoostAI.Infrastructure.Services.EmailService;

public class SystemEmailService(IFluentEmailFactory emailClientFactory, 
    IEnumerable<string> defaultToAddresses) : IEmailService
{
    private readonly string _defaultToAddresses = String.Join(';', defaultToAddresses);
    public async Task SendToAdminAsync(IApplicationNotification applicationNotification)
    {
        try
        {
            var subject = BuildSubjectFromNotification(applicationNotification);
            await emailClientFactory.Create()
                .Subject(subject)
                .Body(applicationNotification.GetMessage())
                .To(_defaultToAddresses)
                .SendAsync();
        }
        catch (Exception ex)
        {
            // Silently fail, do nothing on exception
            // Log this error later
        }
    }
    
    private string BuildSubjectFromNotification(IApplicationNotification applicationNotification)
    {
        var className = applicationNotification.GetType().Name;
        return Regex.Replace(className, "([a-z])([A-Z])", "$1 $2");
    }
}