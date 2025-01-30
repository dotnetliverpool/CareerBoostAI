using System.Net;
using CareerBoostAI.Application.Candidate.Commands.CreateOrUpdateData;
using CareerBoostAI.Application.Services.JsonService;
using CareerBoostAI.Infrastructure.Services.JsonService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class CreateProfile(ILogger<CreateProfile> logger,
    IMediator mediator, 
    [FromKeyedServices("OpenApi")] IJsonService jsonService)
{
    
    [Function(nameof(CreateProfile))]
    [OpenApiOperation(
        operationId: nameof(CreateProfile), 
        tags: [Constants.Tag.Candidate], 
        Summary = "Create a candidate profile", 
        Description = "Creates a new candidate profile with the provided information.")]
    [OpenApiRequestBody(
        contentType: "application/json", 
        bodyType: typeof(CreateOrUpdateProfileCommand), 
        Required = true, 
        Description = "The candidate's profile information.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.Created, 
        contentType: "application/json", 
        bodyType: typeof(string), 
        Summary = "Profile created successfully.", 
        Description = "Returns a success message when the profile is created.")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post",
            Route =  Constants.Route.Candidate.CreateProfile)] HttpRequest req)
    {
           var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
           var command = jsonService.Deserialize<CreateOrUpdateProfileCommand>(requestBody);
           await  mediator.Send(command!);
           return new CreatedResult();
    }
}