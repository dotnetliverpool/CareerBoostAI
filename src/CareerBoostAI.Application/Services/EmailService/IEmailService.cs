namespace CareerBoostAI.Application.Services.EmailService;

public interface IEmailService
{
    Task SendToAdminAsync(IApplicationNotification applicationNotification);
}

