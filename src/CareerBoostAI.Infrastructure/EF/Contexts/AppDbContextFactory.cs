using CareerBoostAI.Infrastructure.EF.Options;
using CareerBoostAI.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CareerBoostAI.Infrastructure.EF.Contexts;

internal class AppDbContextFactory : IDesignTimeDbContextFactory<CareerBoostReadDbContext>
{
    
    
    
    public CareerBoostReadDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("dev.settings.json", optional:true, reloadOnChange: true)
            .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        var mySqlOptions = configuration.GetOptions<MySqlOptions>("MySql");  
        var serverVersion = new MySqlServerVersion(new Version(mySqlOptions.ServerVersion));

        var optionsBuilder = new DbContextOptionsBuilder<CareerBoostReadDbContext>();
        optionsBuilder.UseMySql(
            connectionString: mySqlOptions.ConnectionString,
            serverVersion: serverVersion);

        return new CareerBoostReadDbContext(optionsBuilder.Options);
    }
}