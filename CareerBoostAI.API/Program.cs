

using System.Reflection;
using CareerBoostAI.API.Extensions;
using CareerBoostAI.Application.Extensions;
using CareerBoostAI.Infrastructure;
using CareerBoostAI.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddPersistence();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();

