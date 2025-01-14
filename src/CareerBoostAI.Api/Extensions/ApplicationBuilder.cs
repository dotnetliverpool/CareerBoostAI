using System.Text.Json;
using CareerBoostAI.Api.JsonConverters;
using CareerBoostAI.Api.Middlewares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CareerBoostAI.Api.Extensions;

public static class ApplicationBuilder
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services, Action<IServiceCollection> configure)
    {
        configure(services);
        return services;
    }
    
    
    public static IServiceCollection ConfigureJsonSerialiser(this IServiceCollection services)
    {
        services.Configure<JsonSerializerOptions>(options =>
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.Converters.Add(new DateOnlyConverter());
            }
        );
        return services;
    }
    public static FunctionsApplicationBuilder ConfigureApplication(
        this FunctionsApplicationBuilder app,
        Action<FunctionsApplicationBuilder> configure)
    {
        configure(app);

        return app;
    }
    
    public static FunctionsApplicationBuilder ConfigureAppConfiguration(
        this FunctionsApplicationBuilder app,
        Action<FunctionsApplicationBuilder> configure)
    {
        configure(app);

        return app;
    }
    
    
    public static FunctionsApplicationBuilder AddMiddlewares(this FunctionsApplicationBuilder app)
    {
        app
            .UseWhen<GlobalExceptionHandlingMiddleware>(
                (context) => context.IsHttpTrigger());

        return app;
    }

    public static bool IsHttpTrigger(this FunctionContext context)
    {
        return context.FunctionDefinition.InputBindings.Values
            .First(a => a.Type.EndsWith("Trigger")).Type == "httpTrigger";
    }
}