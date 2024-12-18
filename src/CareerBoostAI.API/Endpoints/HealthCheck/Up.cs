namespace CareerBoostAI.API.Endpoints.HealthCheck;

public class Up: IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/up", () =>
        {
            var status = new ApplicationStatus(DateTime.Now, "Application Active");
            return status;
        });
    }
}

record ApplicationStatus(DateTime Date, string Message)
{
    
}