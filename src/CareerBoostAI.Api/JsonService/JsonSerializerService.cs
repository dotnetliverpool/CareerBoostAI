using Newtonsoft.Json;

namespace CareerBoostAI.Api.JsonService;

public class JsonSerializerService : IJsonSerializerService
{
    public T? Deserialize<T>(string jsonValue)
    {
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new NewtonSoftDateOnlyConverter());
        return JsonConvert.DeserializeObject<T>(jsonValue, settings);
    }

    public string Serialize<T>(T objectValue)
    {
        throw new NotImplementedException();
    }
}