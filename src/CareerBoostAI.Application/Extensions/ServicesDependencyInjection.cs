using System.Reflection;
using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.DocumentSizeService;
using CareerBoostAI.Domain.Candidate.Factories;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Domain.CvContext.Factory;
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
        services.AddSingleton<IDocumentSizeService, AppDocumentSizeService>();
        return services;
    }
}