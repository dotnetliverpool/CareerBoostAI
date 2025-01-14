using System.Net;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
// using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CareerBoostAI.Api.Controllers;

public class CreateCandidateProfile
{
    private readonly IMediator _mediator;

    public CreateCandidateProfile(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Function(nameof(CreateCandidateProfile))]
    [OpenApiOperation(operationId: "createProfile", tags: ["Candidate"], Summary = "Create a candidate profile", Description = "Creates a new candidate profile with the provided information.")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateProfileCommandData), Required = true, Description = "The candidate's profile information.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(string), Summary = "Profile created successfully.", Description = "Returns a success message when the profile is created.")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route =  "candidate/createProfile")] HttpRequest req, ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonSerializer.Deserialize<CreateProfileCommandData>(requestBody);
        var command = new CreateProfileCommand(Id: Guid.NewGuid(), Data: data);
        await _mediator.Send(command);
        return new CreatedResult(location: "None", value: new { Id = command.Id });
    }
}