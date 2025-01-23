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
        operationId: nameof(HealthCheck), 
        tags: [Constants.Tag.Health], 
        Summary = "Perform a health check", 
        Description = "Returns the health status of the application.")]
    [OpenApiResponseWithBody(
        statusCode: HttpStatusCode.OK, 
        contentType: "application/json",
        bodyType: typeof(ApplicationHealthCheck))]
    public Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = "health-check")] HttpRequest req)
    {
        logger.LogInformation("Health Check Run");
        var response = new ApplicationHealthCheck("Healthy", "Health check passed successfully.");
        return Task.FromResult<IActionResult>(new OkObjectResult(response));
    }
}

record ApplicationHealthCheck(string Status, string Message);