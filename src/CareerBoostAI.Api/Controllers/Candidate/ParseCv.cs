using System.Net;
using CareerBoostAI.Application.Candidate.Commands.ParseCv;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class ParseCv(ILogger<ParseCv> logger, IMediator mediator)
{
    [Function(nameof(ParseCv))]
    [OpenApiOperation(operationId: nameof(ParseCv), 
        tags: new[] { Constants.Tag.Candidate },
        Summary = "Parse Contents of CV Document",
        Description = "Extract Content of Cv Document In Structured Json Format")]
    [OpenApiRequestBody(contentType: "multipart/form-data",
        bodyType: typeof(ParseCvDocumentRequest),
        Required = true
        )]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK, 
        contentType: "application/json", 
        bodyType: typeof(string), 
        Summary = "Cv Uploaded Successfully.")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,   "post",
        Route = Constants.Route.Candidate.ParseCv)] HttpRequest req)
    {
        if (!req.ContentType?.Contains("multipart/form-data") ?? false)
        {
            return new BadRequestObjectResult(new {Message = "Invalid content type. Please upload a file."});
        }

        var formCollection = await req.ReadFormAsync();
        var file = formCollection.Files.GetFile("file");

        if (file is null || file.Length == 0)
        {
            return new BadRequestObjectResult(new {Message = "No file uploaded."});
        }
        
        using var documentStream = new MemoryStream();
        await file.CopyToAsync(documentStream);
        documentStream.Position = 0;

        var command = new ParseCvCommand(
            DocumentName: file.FileName, 
            DocumentStream: documentStream);

        await mediator.Send(command);
        return new OkObjectResult("Welcome to Azure Functions!");
        
    }

}

public record ParseCvDocumentRequest(byte[] File);