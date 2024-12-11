using System.Diagnostics;
using Cubicfox.Application;
using Cubicfox.Extensions;
using Cubicfox.Middlewares;
using Cubicfox.Persistence;
using Cubicfox.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Middleware
builder.Services.AddSingleton<GlobalExceptionMiddleware>();
builder.Services.AddSingleton<PerformanceMiddleware>();
builder.Services.AddSingleton<Stopwatch>();

builder.Services.AddHttpClient("ZenquotesAPI", client =>
{
    client.BaseAddress = new Uri("https://zenquotes.io/api/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

using var loggerFactory = LoggerFactory.Create(builder => { });
var serviceScope = app.Services.CreateScope();
//var dataContext = serviceScope.ServiceProvider.GetService<CubicfoxTestContext>();
//dataContext?.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.ConfigureExceptionHandler(loggerFactory.CreateLogger("Exceptions"));
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();

//app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
