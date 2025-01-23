using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class UpdateCv
{
    private readonly ILogger<UpdateCv> _logger;

    public UpdateCv(ILogger<UpdateCv> logger)
    {
        _logger = logger;
    }

    [Function("UpdateCv")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
        
    }

}