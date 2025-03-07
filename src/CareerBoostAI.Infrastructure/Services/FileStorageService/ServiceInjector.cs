﻿using CareerBoostAI.Application.Services;
using Microsoft.Extensions.Azure;
using CareerBoostAI.Infrastructure.Extensions;
using CareerBoostAI.Infrastructure.Services.FileStorageService.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Services.FileStorageService;

public static class ServiceInjector
{
    public static IServiceCollection AddAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<AzureOptions>("Storage:Azure");
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(options.ConnectionString);
        });

        services.AddSingleton<IStorageService, AzureBlobStorageService>();
        return services;
    }

}