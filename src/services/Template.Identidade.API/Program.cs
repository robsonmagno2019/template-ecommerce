using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Template.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets(Assembly.GetAssembly(typeof(Program)));
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddApiConfiguration();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(builder.Environment);

app.Run();
