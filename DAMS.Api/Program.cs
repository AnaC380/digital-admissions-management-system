using Microsoft.EntityFrameworkCore;
using DAMS.Infrastructure.Persistence;
using System.Diagnostics;
using Microsoft.OpenApi; // Opcional, dependendo do uso real

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configurar DbContext
builder.Services.AddDbContext<DamsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Digital Admissions Management System API",
        Version = "v1",
        Description = "API para gerenciar admissões digitais."
    });
});

// Adicionar suporte a CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure o middleware de CORS apenas em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCorsPolicy");

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DAMS API v1");
        c.RoutePrefix = string.Empty; // Swagger será exibido na raiz do aplicativo
    });
}

// app.UseHttpsRedirection();

app.MapControllers();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}