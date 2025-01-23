﻿using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;
using CareerBoostAI.Application.Services.EmailService;
using CareerBoostAI.Infrastructure.EF.Extensions;
using CareerBoostAI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMySqlService(configuration);
        services.AddScoped<IFileStorageService, DummyFileUploadService>();
        services.AddScoped<IEmailSender, DummyEmailSender>();
        services.AddScoped<ICvParserService, DummyCvParserService>();
        
        return services;
    }
}