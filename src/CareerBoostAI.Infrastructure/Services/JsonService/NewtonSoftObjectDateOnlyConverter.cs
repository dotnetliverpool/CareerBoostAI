using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CareerBoostAI.Infrastructure.Services.JsonService;

public class NewtonSoftObjectDateOnlyConverter : JsonConverter<DateOnly?>
{
    public override void WriteJson(JsonWriter writer, DateOnly? value, JsonSerializer serializer)
    {
        
        if (value == null)
        {
            writer.WriteNull();
            return;
        }
        
        var jsonObject = new JObject
        {
            { "year", value.Value.Year },
            { "month", value.Value.Month },
            { "day", value.Value.Day }
        };

        jsonObject.WriteTo(writer);
    }

    public override DateOnly? ReadJson(JsonReader reader, Type objectType, DateOnly? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }
        if (reader.TokenType == JsonToken.StartObject)
        {
            var jsonObject = JObject.Load(reader);

            // Extract year, month, and day
            int year = jsonObject["year"]?.Value<int>() ?? throw new JsonSerializationException("Missing 'year' property.");
            int month = jsonObject["month"]?.Value<int>() ?? throw new JsonSerializationException("Missing 'month' property.");
            int day = jsonObject["day"]?.Value<int>() ?? throw new JsonSerializationException("Missing 'day' property.");
            {
                return new DateOnly(year, month, day);
            }
        }

        throw new JsonSerializationException("Expected start of object.");
    }
}