using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerBoostAI.Api.Controllers.Candidate;

public class ParseCv(ILogger<ParseCv> logger)
{
    [Function("ParseCv")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
        
    }

}