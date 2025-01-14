using System.Net;
using System.Text.Json;
using CareerBoostAI.Shared.Abstractions.Exceptions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace CareerBoostAI.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IFunctionsWorkerMiddleware
{
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
            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, Message = "An unexpected error occurred" });
            await response.WriteStringAsync(json);
            context.GetInvocationResult().Value = response;
        }
        
    }
    
    public static string ToUnderscoreCase(string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i-1]) ? $"_{x}" : x.ToString())).ToLower();
}