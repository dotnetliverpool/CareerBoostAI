using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using CareerBoostAI.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Hosting;

namespace CareerBoostAI.Api.Middlewares;

public record ErrorResponse( string ErrorCode, string Message, string? StackTrace = null);

public class GlobalExceptionHandlingMiddleware(IWebHostEnvironment env) : IFunctionsWorkerMiddleware
{
    private IWebHostEnvironment _env = env;

    private JsonSerializerOptions JsonSerializerOptions
        => new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull};

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CareerBoostAIException ex)
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
        catch (Exception exc)
        {
            var request = await context.GetHttpRequestDataAsync();
            var response = request!.CreateResponse();
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Headers.Add("content-type", "application/json");
            var errorCode = "Internal_Server_Error";
            var message = true
                ? new ErrorResponse(
                    ErrorCode: errorCode,
                    Message: exc.Message,
                    StackTrace: exc.StackTrace
                )
                : new ErrorResponse(
                    ErrorCode: errorCode,
                    Message: "An unexpected error occurred. Please contact support if the issue persists."
                );
            
            var json = JsonSerializer.Serialize(message, JsonSerializerOptions);
            await response.WriteStringAsync(json);
            context.GetInvocationResult().Value = response;
        }
        
    }
    
    public static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i-1]) ? $"_{x}" : x.ToString())).ToLower();
}