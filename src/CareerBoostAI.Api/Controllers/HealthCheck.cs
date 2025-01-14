using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace CareerBoostAI.Api.Functions;

public class HealthCheck
{
    private readonly ILogger<HealthCheck> _logger;
    
    public HealthCheck(ILogger<HealthCheck> logger)
    {
        _logger = logger;
    }
    
    
    [FunctionName("Up")]
    [OpenApiOperation(operationId: "HealthCheck", tags: new[] { "Health" }, Summary = "Perform a health check", Description = "Returns the health status of the application.")]
    [OpenApiResponseWithBody(statusCode: System.Net.HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ApplicationHealthCheck), Description = "The health status response.")]
    public async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get",  Route = "health-check")] HttpRequest req)
    {
        _logger.LogInformation("Health Check Run");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var response = new ApplicationHealthCheck("Healthy", "Health check passed successfully.");
        return new OkObjectResult(response);
    }
}

record ApplicationHealthCheck(string Status, string Message);