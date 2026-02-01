using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Load .env file into environment variables if present (do not override existing variables)
var envFile = Path.Combine(builder.Environment.ContentRootPath ?? Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envFile))
{
    foreach (var rawLine in File.ReadAllLines(envFile))
    {
        var line = rawLine.Trim();
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            continue;

        var idx = line.IndexOf('=');
        if (idx <= 0) continue;

        var key = line.Substring(0, idx).Trim();
        var val = line.Substring(idx + 1).Trim();

        // Remove optional quotes
        if ((val.StartsWith("\"") && val.EndsWith("\"")) || (val.StartsWith("'") && val.EndsWith("'")))
            val = val.Substring(1, val.Length - 2);

        // Do not override existing environment variables
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
        {
            Environment.SetEnvironmentVariable(key, val);
        }
    }
}

// Build connection string: prefer explicit DEFAULT_CONNECTION env var, then individual DB_* vars, then appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var envConnection = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
if (!string.IsNullOrWhiteSpace(envConnection))
{
    connectionString = envConnection;
}
else
{
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var dbUser = Environment.GetEnvironmentVariable("DB_USER");
    var dbPass = Environment.GetEnvironmentVariable("DB_PASS");

    if (!string.IsNullOrWhiteSpace(dbHost) && !string.IsNullOrWhiteSpace(dbName) && !string.IsNullOrWhiteSpace(dbUser))
    {
        connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPass};";
    }
}

// Configure DbContext (MySQL via Pomelo)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories and services
builder.Services.AddScoped<MinhaApi.Repositories.Interfaces.IAgendamentoRepository, MinhaApi.Repositories.Implementations.AgendamentoRepository>();
builder.Services.AddScoped<MinhaApi.Repositories.Interfaces.IUsuarioRepository, MinhaApi.Repositories.Implementations.UsuarioRepository>();

builder.Services.AddScoped<MinhaApi.Repositories.Interfaces.IServicoRepository, MinhaApi.Repositories.Implementations.ServicoRepository>();
builder.Services.AddScoped<MinhaApi.Repositories.Interfaces.IProfissionalRepository, MinhaApi.Repositories.Implementations.ProfissionalRepository>();

builder.Services.AddScoped<MinhaApi.Services.Interfaces.IAgendamentoService, MinhaApi.Services.Implementations.AgendamentoService>();
builder.Services.AddScoped<MinhaApi.Services.Interfaces.IUsuarioService, MinhaApi.Services.Implementations.UsuarioService>();

builder.Services.AddScoped<MinhaApi.Services.Interfaces.IServicoService, MinhaApi.Services.Implementations.ServicoService>();
builder.Services.AddScoped<MinhaApi.Services.Interfaces.IProfissionalService, MinhaApi.Services.Implementations.ProfissionalService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Map controllers so Swagger shows the controller endpoints
app.MapControllers();

app.Run();
