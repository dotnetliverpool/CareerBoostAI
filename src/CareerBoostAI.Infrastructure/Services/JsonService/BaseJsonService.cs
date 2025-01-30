using CareerBoostAI.Application.Services.JsonService;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CareerBoostAI.Infrastructure.Services.JsonService;

public abstract class BaseJsonService : IJsonService
{
    protected abstract JsonSerializerSettings GetSettings();
    
    public T? Deserialize<T>(string jsonValue)
    {
        
        return JsonConvert.DeserializeObject<T>(jsonValue, GetSettings());
    }

    public string Serialize<T>(T objectValue)
    {
        throw new NotImplementedException();
    }
}

public enum JsonServices
{
    System,
    OpenApi
}

public sealed class SystemJsonService: BaseJsonService
{
    protected override JsonSerializerSettings GetSettings()
    {
        var settings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new DefaultContractResolver {NamingStrategy = new CamelCaseNamingStrategy()}
        };
        settings.Converters.Add(new NewtonSoftStringDateOnlyConverter());
        return settings;
    }
}

public sealed class OpenApiJsonService : BaseJsonService
{
    protected override JsonSerializerSettings GetSettings()
    {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new NewtonSoftObjectDateOnlyConverter());
        return settings;
    }
}