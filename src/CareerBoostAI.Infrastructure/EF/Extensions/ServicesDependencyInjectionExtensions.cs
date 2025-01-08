using CareerBoostAI.Infrastructure.EF.Contexts;
using CareerBoostAI.Infrastructure.EF.Options;
using CareerBoostAI.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerBoostAI.Infrastructure.EF.Extensions;

public static class ServicesDependencyInjectionExtensions
{
    public static IServiceCollection AddMySqlService(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlOptions = configuration.GetOptions<MySqlOptions>("MySql");
        var severVersion = new MySqlServerVersion(new Version(mySqlOptions.ServerVersion));
        
        services.AddDbContext<CareerBoostDbContext>(options =>
        {
            options.UseMySql(mySqlOptions.ConnectionString, severVersion);
        });
    
        
        
        
       
        return services;
    }
}