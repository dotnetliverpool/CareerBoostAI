using System.Reflection;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CandidateContext.Services;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;
using CareerBoostAI.Domain.UploadContext;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Application.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddTransient<ICandidateFactory, CandidateFactory>();
        services.AddTransient<ICvFactory, CvFactory>();
        services.AddTransient<IUploadFactory, UploadFactory>();
        services.AddSingleton<IDocumentConstraintsService, AppDocumentConstraintsService>();
        services.AddTransient<ICandidateProfileUpdateDomainService, CandidateProfileUpdateService>();
        services.AddTransient<ICvUpdateService, CvUpdateService>();
        return services;
    }
}