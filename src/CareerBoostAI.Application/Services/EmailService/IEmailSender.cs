namespace CareerBoostAI.Application.Services.EmailService;

public interface IEmailSender
{
    Task SendToAdminAsync(IApplicationNotification applicationNotification);
}

