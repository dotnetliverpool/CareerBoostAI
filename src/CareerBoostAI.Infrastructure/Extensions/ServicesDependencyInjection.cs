using CareerBoostAI.Application.Services;
using CareerBoostAI.Application.Services.CvParseService;

using CareerBoostAI.Application.Services.JsonService;
using CareerBoostAI.Domain.Common.Services;
using CareerBoostAI.Infrastructure.EF.Extensions;
using CareerBoostAI.Infrastructure.Services;
using CareerBoostAI.Infrastructure.Services.AiClient;
using CareerBoostAI.Infrastructure.Services.CvContentParser;
using CareerBoostAI.Infrastructure.Services.EmailService;
using CareerBoostAI.Infrastructure.Services.FileStorageService;
using CareerBoostAI.Infrastructure.Services.JsonService;
using CareerBoostAI.Infrastructure.Services.OcrService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.Extensions;

public static class ServicesDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMySqlService(configuration);
        // services.AddAzureBlobStorage(configuration);
        services.AddSingleton<IStorageService, DummyUploadService>();
        services.AddOpenAiClient(configuration);
        services.AddFluentSmtpEmail(configuration);
        services.AddScoped<IOcrService, AppOcrService>();
        services.AddScoped<ICvDocumentContentParser, OpenAiCvContentParser>();
        services.AddKeyedSingleton<IJsonService, SystemJsonService>(JsonServices.System.ToString());
        services.AddKeyedSingleton<IJsonService, OpenApiJsonService>(JsonServices.OpenApi.ToString());
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        return services;
    }
}