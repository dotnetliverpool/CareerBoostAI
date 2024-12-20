using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CareerBoostAI.Functions.Functions;

public class CreateCandidateProfileFunction
{
    private readonly IMediator _mediator;

    public CreateCandidateProfileFunction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [FunctionName("CreateCandidateProfile")]
    [OpenApiOperation(operationId: "createProfile", tags: new[] { "Candidate" }, Summary = "Create a candidate profile", Description = "Creates a new candidate profile with the provided information.")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateProfileCommand), Required = true, Description = "The candidate's profile information.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Summary = "Profile created successfully.", Description = "Returns a success message when the profile is created.")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "candidate/createProfile")] HttpRequest req, ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var command = JsonConvert.DeserializeObject<CreateProfileCommand>(requestBody);
        await _mediator.Send(command);
        return new OkObjectResult("Profile created successfully.");
    }
}