using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class ParseCv
{
    private readonly ILogger<ParseCv> _logger;

    public ParseCv(ILogger<ParseCv> logger)
    {
        _logger = logger;
    }

    [Function("ParseCv")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
        
    }

}