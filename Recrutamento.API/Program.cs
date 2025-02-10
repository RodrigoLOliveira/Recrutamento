using Microsoft.Extensions.DependencyInjection;
using Recrutamento.API.Interfaces;
using Recrutamento.API.Services;
using Recrutamento.API.Settings;
using Recrutamento.Infra.Interfaces;
using Recrutamento.Infra.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Settings

builder.AddContextSettings();
builder.JwtTokenConfigure();

// Services
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
