using System.Reflection;
using CareerBoostAI.Application.Services.DocumentConstraintsService;
using CareerBoostAI.Domain.CandidateContext.Factories;
using CareerBoostAI.Domain.CvContext.Factory;
using CareerBoostAI.Domain.CvContext.Services;
using CareerBoostAI.Domain.UploadContext;
using CareerBoostAI.Domain.UploadContext.ValueObjects;
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
        
        services.AddSingleton<ICandidateFactory, CandidateFactory>();
        services.AddSingleton<ICvFactory, CvFactory>();
        services.AddSingleton<IUploadFactory, UploadFactory>();
        services.AddSingleton<IDocumentConstraintsService, AppDocumentConstraintsService>();
        services.AddSingleton<ICvUpdateService, CvUpdateService>();
        return services;
    }
}