using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        return services;
    }
}