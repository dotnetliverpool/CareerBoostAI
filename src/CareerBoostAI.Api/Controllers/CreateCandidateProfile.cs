using System.Net;
using CareerBoostAI.Api.JsonService;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CareerBoostAI.Api.Controllers;

public class CreateCandidateProfile
{
    private readonly IMediator _mediator;
    private readonly IJsonSerializerService _jsonSerializerService;

    public CreateCandidateProfile(IMediator mediator, IJsonSerializerService jsonSerializerService)
    {
        _mediator = mediator;
        _jsonSerializerService = jsonSerializerService;
    }

    [Function(nameof(CreateCandidateProfile))]
    [OpenApiOperation(operationId: "createProfile", tags: ["Candidate"], Summary = "Create a candidate profile", Description = "Creates a new candidate profile with the provided information.")]
    [OpenApiRequestBody(
        contentType: "application/json", 
        bodyType: typeof(CreateProfileCommand), 
        Required = true, 
        Description = "The candidate's profile information.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.Created, 
        contentType: "application/json", 
        bodyType: typeof(string), 
        Summary = "Profile created successfully.", 
        Description = "Returns a success message when the profile is created.")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route =  "candidate/createProfile")] HttpRequest req, ILogger log)
    {
           string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
           var data = _jsonSerializerService.Deserialize<CreateProfileCommand>(requestBody);
           var command = data! with
           {
               Id = Guid.NewGuid()
           };
           await _mediator.Send(command);
           return new CreatedResult(location: "None", value: new { Id = command.Id });
    }
}