﻿using CareerBoostAI.Application.Extensions;
using CareerBoostAI.Functions;
using CareerBoostAI.Infrastructure;
using CareerBoostAI.Persistence;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly:FunctionsStartup(typeof(Startup))]
namespace CareerBoostAI.Functions;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var config = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddPersistence();
    }

    private IConfiguration BuildConfiguration(string applicationRootPath)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(applicationRootPath)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        return configuration;
    }
}