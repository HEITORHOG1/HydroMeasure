using HydroMeasure.Application.Commands.Alertas.Create;
using HydroMeasure.Application.Commands.Condominios.Create;
using HydroMeasure.Application.Commands.ConfiguracoesCondominio.Create;
using HydroMeasure.Application.Commands.Hidrometros.Create;
using HydroMeasure.Application.Commands.Leituras.Create;
using HydroMeasure.Application.Commands.Relatorios.Create;
using HydroMeasure.Application.Commands.Unidades.Create;
using HydroMeasure.Application.Mappings;
using HydroMeasure.Application.Services;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------------------------------------------------------
// Database Context Configuration
// ----------------------------------------------------------------------------
builder.Services.AddDbContext<HydroMeasureDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
// ----------------------------------------------------------------------------
// MediatR Configuration
// ----------------------------------------------------------------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateCondominioCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateUnidadeCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateHidrometroCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateLeituraCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateConfiguracaoCondominioCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(GerarRelatorioCommand).Assembly });
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateAlertaCommand).Assembly });
});

// ----------------------------------------------------------------------------
// Repositories and Unit of Work Registration
// ----------------------------------------------------------------------------
builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
builder.Services.AddScoped<IUnidadeRepository, UnidadeRepository>();
builder.Services.AddScoped<IHidrometroRepository, HidrometroRepository>();
builder.Services.AddScoped<ILeituraRepository, LeituraRepository>();
builder.Services.AddScoped<IConfiguracaoCondominioRepository, ConfiguracaoCondominioRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<IConfiguracaoSistemaRepository, ConfiguracaoSistemaRepository>();

// Register other repositories here...
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ----------------------------------------------------------------------------
// AutoMapper Configuration
// ----------------------------------------------------------------------------
builder.Services.AddAutoMapper(typeof(CondominioProfile));
builder.Services.AddAutoMapper(typeof(UnidadeProfile));
builder.Services.AddAutoMapper(typeof(HidrometroProfile));
builder.Services.AddAutoMapper(typeof(LeituraProfile));
builder.Services.AddAutoMapper(typeof(ConfiguracaoCondominioProfile));
builder.Services.AddAutoMapper(typeof(RelatorioProfile));
builder.Services.AddAutoMapper(typeof(AlertaProfile));

// ----------------------------------------------------------------------------
// Controllers (REST API) Configuration
// ----------------------------------------------------------------------------
builder.Services.AddControllers();

// ----------------------------------------------------------------------------
// Swagger / OpenAPI Configuration for REST APIs
// ----------------------------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HydroMeasure API", Version = "v1" });
    // You can add more Swagger configurations here, like security definitions, etc.
});

// ----------------------------------------------------------------------------
// Background Services Configuration
// ----------------------------------------------------------------------------
builder.Services.AddHostedService<AlertaGenerationService>();

// ----------------------------------------------------------------------------
// gRPC Configuration
// ----------------------------------------------------------------------------
builder.Services.AddGrpc(); // Add gRPC services

// ----------------------------------------------------------------------------
// Build the Application
// ----------------------------------------------------------------------------
var app = builder.Build();

// ----------------------------------------------------------------------------
// Configure the HTTP request pipeline.
// ----------------------------------------------------------------------------

// Swagger for REST API Documentation (only in Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HydroMeasure API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root URL (https://localhost:xxxx/)
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ----------------------------------------------------------------------------
// Endpoint Mapping
// ----------------------------------------------------------------------------
app.MapControllers(); // Map REST Controllers

// Map gRPC Services

// Informational gRPC Endpoint for Browsers (optional - for testing in browser)
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. " +
        "To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
});

// ----------------------------------------------------------------------------
// Run the application
// ----------------------------------------------------------------------------
app.Run();