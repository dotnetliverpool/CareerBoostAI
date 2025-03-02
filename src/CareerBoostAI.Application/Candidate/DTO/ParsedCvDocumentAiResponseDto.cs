using System.Text.Json.Nodes;
using CareerBoostAI.Application.Common.Abstractions.AiDto;

namespace CareerBoostAI.Application.Candidate.DTO;

public class ParsedCvDocumentAiResponseDto : BaseAiResponseSchemaDto
{
    protected override string GetStringSchema()
    {
        return """
{
    "type": "object",
    "properties": {
        "summary": { "type": "string" },
        "educations": {
            "type": "array",
            "items": {
                "type": "object",
                "properties": {
                    "organisationName": { "type": "string" },
                    "city": { "type": "string" },
                    "country": { "type": "string" },
                    "startDate": { "type": "string" },
                    "endDate": { "type": "string" },
                    "program": { "type": "string" },
                    "grade": { "type": "string" }
                },
                "required": ["organisationName", "city", "country", "startDate", "endDate", "program", "grade"],
                "additionalProperties": false
            }
        },
        "experiences": {
            "type": "array",
            "items": {
                "type": "object",
                "properties": {
                    "organisationName": { "type": "string"},
                    "city": { "type": "string" },
                    "country": { "type": "string" },
                    "startDate": { "type": "string"},
                    "endDate": { "type": "string"},
                    "description": { "type": "string" }
                },
                "required": ["organisationName", "city", "country", "startDate", "endDate", "description"],
                "additionalProperties": false
            }
        },
        "skills": {
            "type": "array",
            "items": { "type": "string" }
        },
        "languages": {
            "type": "array",
            "items": { "type": "string" }
        }
    },
    "required": ["summary", "educations", "experiences", "skills", "languages"],
    "additionalProperties": false
}
""";
    }

    protected override JsonNode GetJsonNode()
    {
        var schema = GetStringSchema();
        return BuildNodeFromString(schema);
    }
}