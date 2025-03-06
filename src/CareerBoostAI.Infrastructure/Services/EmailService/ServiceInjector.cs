using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Infrastructure.Extensions;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Services.EmailService;

public static class ServiceInjector
{
    
    
    public static IServiceCollection AddFluentSmtpEmail(
        this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<EmailOptions>("Email:Smtp");
        services
            .AddFluentEmail(options.DefaultFromAddress)
            .AddSmtpSender(options.Host, options.Port, options.Username, options.Password);
        services.AddScoped<IEmailService>(provider => 
            new SystemEmailService(
                provider.GetRequiredService<IFluentEmailFactory>(), 
                options.DefaultToAddresses?.Split(',').ToList() ?? new List<string>()));
        return services;
    }
}