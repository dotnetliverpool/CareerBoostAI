﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Application.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(AssemblyReference.Assembly));
        return services;
    }
}