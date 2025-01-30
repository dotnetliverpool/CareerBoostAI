using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using CareerBoostAI.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace CareerBoostAI.Api.Middlewares;

public record ErrorResponse(string ErrorCode, string Message, string? StackTrace = null);

public class GlobalExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
    private bool IsEnvironment(string environment)
        => string.Equals(
            Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT"), 
            environment, StringComparison.OrdinalIgnoreCase);
    private JsonSerializerOptions JsonSerializerOptions
        => new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
    
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CareerBoostAiDomainException ex)
        {
            await HandleUserError(context, ex);
        }
        catch (CareerBoostAiApplicationException ex)
        {
            await HandleUserError(context, ex);
        }
        catch (CareerBoostAiInfrastructureException ex)
        {
            await HandleSystemError(context, ex);
        }
        catch (Exception exc)
        {
            await HandleSystemError(context, exc);
        }
    }

    private static async Task HandleUserError(FunctionContext context, CareerBoostAiException ex)
    {
        var request = await context.GetHttpRequestDataAsync();
        var response = request!.CreateResponse();
        response.StatusCode = HttpStatusCode.BadRequest;
        response.Headers.Add("content-type", "application/json");
        var errorCode = ToUnderscoreCase(ex.GetType().Name.Replace("Exception", string.Empty));
        var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
        await response.WriteStringAsync(json);
        context.GetInvocationResult().Value = response;
    }
    
    private async Task HandleSystemError(FunctionContext context, Exception exc)
    {
        var request = await context.GetHttpRequestDataAsync();
        var response = request!.CreateResponse();
        response.StatusCode = HttpStatusCode.InternalServerError;
        response.Headers.Add("content-type", "application/json");
        var errorCode = "Internal_Server_Error";
        var message = IsEnvironment("Development")
            ? new ErrorResponse(
                errorCode,
                exc.Message,
                exc.StackTrace
            )
            : new ErrorResponse(
                errorCode,
                "An unexpected error occurred. Please contact support if the issue persists."
            );

        var json = JsonSerializer.Serialize(message, JsonSerializerOptions);
        await response.WriteStringAsync(json);
        context.GetInvocationResult().Value = response;
    }

    private static string ToUnderscoreCase(string value)
    {
        return string.Concat((value).Select((x, i) =>
            i > 0 && char.IsUpper(x) && !char.IsUpper(value![i - 1]) ? $"_{x}" : x.ToString())).ToLower();
    }
}