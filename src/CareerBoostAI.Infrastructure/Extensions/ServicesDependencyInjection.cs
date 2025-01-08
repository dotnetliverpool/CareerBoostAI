using CareerBoostAI.Application.Candidate;
using CareerBoostAI.Application.Common.Abstractions;
using CareerBoostAI.Infrastructure.EF.Extensions;
using CareerBoostAI.Infrastructure.EF.Repositories;
using CareerBoostAI.Infrastructure.EF.Transaction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMySqlService(configuration);
        services.AddScoped<ICandidateRepository, PostgresCandidateRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}