using Azure.Core.Serialization;
using CareerBoostAI.Application.Extensions;
using CareerBoostAI.Api.Extensions;
using CareerBoostAI.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(app =>
    {
        app.Configuration.AddJsonFile("dev.settings.json", optional:true, reloadOnChange: true);
    });

builder.Services
    .ConfigureServices(s =>
    {
        s.AddApplication();
        s.AddInfrastructure(builder.Configuration);
        s.RegisterApiServices();
    });


builder
    .ConfigureApplication(app =>
    {
        app.AddMiddlewares();
    });


await builder.Build().RunAsync();