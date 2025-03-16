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
            .AddEnvironmentVariables() 
            .Build();
        var mySqlOptions = configuration.GetOptions<MySqlOptions>("Database:MySql");
        
        var severVersion = new MySqlServerVersion(new Version(mySqlOptions.ServerVersion));
        var optionsBuilder = new DbContextOptionsBuilder<CareerBoostReadDbContext>();
        optionsBuilder
            .UseMySql(connectionString: mySqlOptions.ConnectionString, serverVersion: severVersion);
        

        return new CareerBoostReadDbContext(optionsBuilder.Options);
    }
}