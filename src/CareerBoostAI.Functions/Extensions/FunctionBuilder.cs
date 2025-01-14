using CareerBoostAI.Functions.Middlewares;
using Microsoft.Azure.WebJobs.Host.Timers;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Functions.Extensions;

public static class FunctionBuilder
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        return services;
    }
}