using System.Reflection;
using CareerBoostAI.Domain.Candidate.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Application.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddSingleton<ICandidateFactory, CandidateFactory>();
        services.AddSingleton<ICandidateCvFactory, CandidateCvFactory>();
        return services;
    }
}