﻿using CareerBoostAI.Infrastructure.EF.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMySqlService(configuration);
        
        
        return services;
    }
}