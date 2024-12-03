using StudentGradings.API.Configuration;
using StudentGradings.BLL.Configuration;
using StudentGradings.DAL.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsetings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsetings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsetings.secrets.json", optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .AddEnvironmentVariables()
    .Build();

var configuration = builder.Configuration;

builder.Services.AddApiServices();
builder.Services.AddBllServices();
builder.Services.AddDalServices(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();
