using CareerBoostAI.Infrastructure.Common.Exception;
using Microsoft.Extensions.Configuration;

namespace CareerBoostAI.Infrastructure.Extensions;

public static class OptionsExtensions
{
    public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
        where TOptions: new()
    {
        var section = configuration.GetSection(sectionName);

        if (!section.Exists())
        {
            throw new ConfigurationSectionNotFoundException(sectionName);
        }

        var options = new TOptions();
        section.Bind(options);
        
        // Validate required properties
        // var properties = typeof(TOptions).GetProperties();
        // foreach (var property in properties)
        // {
        //     if (property.GetCustomAttribute<RequiredAttribute>() == null)
        //     {
        //         throw new InvalidOperationException($"The required configuration value for {property.Name} is missing.");
        //     }
        //     
        //     var value = property.GetValue(options);
        //     if (value == null || (value is string str && string.IsNullOrEmpty(str)))
        //     {
        //         throw new InvalidOperationException($"The required configuration value for {property.Name} is missing.");
        //     }
        // }
        
        return options;
    }
}