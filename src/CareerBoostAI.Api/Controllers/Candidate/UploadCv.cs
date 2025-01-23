using System.Net;
using CareerBoostAI.Api.JsonService;
using CareerBoostAI.Application.Candidate.Commands.UploadCvDocument;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class UploadCv(ILogger<UploadCv> logger, IMediator mediator)
{
    [Function(nameof(UploadCv))]
    [OpenApiOperation(
        operationId:nameof(UploadCv), tags: [Constants.Tag.Candidate],
        Summary = "Upload Candidate CV",
        Description = "Uploads the CV information for a candidate.")]
    [OpenApiRequestBody(
        contentType: "multipart/form-data", 
        bodyType: typeof(IFormFile), 
        Required = true, 
        Description = "The candidate's CV file to be uploaded.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK, 
        contentType: "application/json", 
        bodyType: typeof(OkObjectResult), 
        Summary = "Cv Uploaded Successfully.", 
        Description = "Returns a success message when the CV has been uploaded.")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", 
        Route = Constants.Route.Candidate.UploadCv)] HttpRequest req)
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
        
        if (string.IsNullOrEmpty(formCollection["email"]))
        {
            return new BadRequestObjectResult(new {Message = "Email is required."});
        }

        using var documentStream = new MemoryStream();
        await file.CopyToAsync(documentStream);
        documentStream.Position = 0;

        var command = new UploadCvDocumentCommand(
            Email: formCollection["email"]!, 
            DocumentName: file.FileName, 
            DocumentStream: documentStream);

        await mediator.Send(command);

        return new OkObjectResult(new {Message = $"CV uploaded {command.DocumentName} " +
                                                 $"successfully for {command.Email}"});
    }
}
