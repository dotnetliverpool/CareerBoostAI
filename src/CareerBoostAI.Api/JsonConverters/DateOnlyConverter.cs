using System.Text.Json;
using System.Text.Json.Serialization;

namespace CareerBoostAI.Api.JsonConverters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var dateStr = reader.GetString();
            if (DateOnly.TryParse(dateStr, out var dateOnly))
            {
                return dateOnly;
            }
        }
        if (reader.TokenType == JsonTokenType.StartObject)
        {
            var jsonObject = JsonDocument.ParseValue(ref reader).RootElement;

            // Extract year, month, and day
            var year = jsonObject.GetProperty("year").GetInt32();
            var month = jsonObject.GetProperty("month").GetInt32();
            var day = jsonObject.GetProperty("day").GetInt32();

            // Create and return a DateOnly object
            return new DateOnly(year, month, day);
        }

        throw new JsonException("Invalid DateOnly format.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        // writer.WriteStringValue(value.ToString("yyyy-MM-dd")); // Use your desired format here
        writer.WriteStartObject();
        writer.WriteNumber("year", value.Year);
        writer.WriteNumber("month", value.Month);
        writer.WriteNumber("day", value.Day);
        writer.WriteEndObject();
    }
}
