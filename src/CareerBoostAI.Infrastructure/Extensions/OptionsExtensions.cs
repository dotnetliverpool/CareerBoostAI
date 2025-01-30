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
        return options;
    }
}