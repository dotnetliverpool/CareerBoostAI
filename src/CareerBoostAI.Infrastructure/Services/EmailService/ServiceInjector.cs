using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Infrastructure.Extensions;
using FluentEmail.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Services.EmailService;

public static class ServiceInjector
{
    public static IServiceCollection AddSmtpFluentEmail(
        this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<SmtpEmailOptions>("Email:Smtp");
        services
            .AddFluentEmail(options.DefaultFromAddress)
            .AddSmtpSender(options.Host, options.Port);
        services.AddScoped<IEmailService>(provider => 
            new SystemEmailService(
                provider.GetRequiredService<IFluentEmailFactory>(), 
                options.DefaultToAddresses));
        return services;
    }
    
    public static IServiceCollection AddGoogleFluentEmail(
        this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<GoogleMailOptions>("Email:GoogleMail");
        services
            .AddFluentEmail(options.DefaultFromAddress)
            .AddSmtpSender(options.Host, options.Port, options.Username, options.Password);
        services.AddScoped<IEmailService>(provider => 
            new SystemEmailService(
                provider.GetRequiredService<IFluentEmailFactory>(), 
                options.DefaultToAddresses));
        return services;
    }
}