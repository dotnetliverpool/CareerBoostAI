using System.Text.Json.Nodes;

namespace CareerBoostAI.Application.Common.Abstractions.AiDto;


public interface IAiResponseSchemaDto
{
    public BinaryData Serialize();
}
public abstract class BaseAiResponseSchemaDto : IAiResponseSchemaDto
{
    
    protected virtual string GetStringSchema() => String.Empty;
    protected virtual object GetObjectSchema() => new { };
    protected abstract JsonNode GetJsonNode();

    protected JsonNode BuildNodeFromString(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
            throw new ArgumentException("JSON string cannot be null or empty.", nameof(jsonString));

        var jsonNode = JsonNode.Parse(jsonString);
        if (jsonNode == null)
            throw new ArgumentException("Invalid JSON format.", nameof(jsonString));
        return jsonNode;
    }
    public BinaryData Serialize()
    {
        return BinaryData.FromString(GetJsonNode().ToJsonString());
    }
}