using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace CareerBoostAI.Api.Controllers;

public class HealthCheck(ILogger<HealthCheck> logger)
{
    [Function(nameof(HealthCheck))]
    [OpenApiOperation(
        operationId: "HealthCheck", 
        tags: new[] { "Health" }, 
        Summary = "Perform a health check", 
        Description = "Returns the health status of the application.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK, 
        contentType: "application/json",
        bodyType: typeof(ApplicationHealthCheck))]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "health-check")] HttpRequest _)
    {
        logger.LogInformation("Health Check Run");
        var response = new ApplicationHealthCheck("Healthy", "Health check passed successfully.");
        return new OkObjectResult(response);
    }
}

record ApplicationHealthCheck(string Status, string Message);