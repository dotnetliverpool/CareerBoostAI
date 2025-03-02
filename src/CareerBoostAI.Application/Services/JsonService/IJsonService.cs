namespace CareerBoostAI.Application.Services.JsonService;

public interface IJsonService
{
    public T? Deserialize<T>(string jsonValue);

    public string Serialize<T>(T objectValue);
}