using System.Net;
using CareerBoostAI.Api.JsonService;
using CareerBoostAI.Application.Candidate.Commands.UpdateCvContent;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class UpdateCv(ILogger<UpdateCv> logger,
    IMediator mediator, IJsonSerializerService jsonSerializerService)
{
    [Function(nameof(UpdateCv))]
    [OpenApiOperation(
        operationId:nameof(UpdateCv), tags: [Constants.Tag.Candidate],
        Summary = "Update Candidate CV",
        Description = "Updates the CV information for a candidate with the specified email address.")]
    [OpenApiRequestBody(
        contentType: "application/json", 
        bodyType: typeof(UpdateCvCommand), 
        Required = true, 
        Description = "The candidate's Cv information to be updated.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK, 
        contentType: "application/json", 
        bodyType: typeof(OkObjectResult), 
        Summary = "Cv Information Updated Successfully.", 
        Description = "Returns a success message when the cv information has been changed.")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", 
        Route = Constants.Route.Candidate.UpdateCv)] HttpRequest req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var command = jsonSerializerService.Deserialize<UpdateCvCommand>(requestBody);
        await mediator.Send(command!);
        return new OkObjectResult(new {Message = $"Cv Updated successfully for {command!.Email}"});
        
    }

}