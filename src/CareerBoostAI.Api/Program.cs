using CareerBoostAI.Application.Extensions;
using CareerBoostAI.Api.Extensions;
using CareerBoostAI.Infrastructure.Extensions;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
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
    });

builder
    .ConfigureApplication(app =>
    {
        app.AddMiddlewares();
    });


await builder.Build().RunAsync();