using System;
using System.IO;
using System.Threading.Tasks;
using CareerBoostAI.Application.Candidate.Commands.CreateProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous,  "post", Route = "candidate/createProfile")] HttpRequest req, ILogger log)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var command = JsonConvert.DeserializeObject<CreateProfileCommand>(requestBody);
        await _mediator.Send(command);
        return new OkObjectResult("Profile created successfully.");
    }
}