namespace CareerBoostAI.Api.JsonService;

public interface IJsonSerializerService
{
    public T? Deserialize<T>(string jsonValue);

    public string Serialize<T>(T objectValue);
}