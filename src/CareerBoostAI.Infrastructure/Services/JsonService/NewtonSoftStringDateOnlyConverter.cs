
using Newtonsoft.Json;

namespace CareerBoostAI.Infrastructure.Services.JsonService;

public class NewtonSoftStringDateOnlyConverter : JsonConverter<DateOnly?>
{
    private readonly string _dateFormat = "yyyy-MM-dd";
    
    public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToString(_dateFormat));
    }

    public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var value = (string)reader.Value!;
        try
        {
            return DateOnly.ParseExact(value, _dateFormat);
        }
        catch (Exception)
        {
            return null;
        }
    }
}