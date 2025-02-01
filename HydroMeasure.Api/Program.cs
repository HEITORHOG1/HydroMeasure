using HydroMeasure.Application.Commands.Condominios.Create;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Infrastructure.Context;
using HydroMeasure.Infrastructure.Repositories;
using HydroMeasure.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext usando SQLite
builder.Services.AddDbContext<HydroMeasureDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

// Registro do MediatR (apontando para a assembly onde estão os comandos e handlers)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(new[] { typeof(CreateCondominioCommand).Assembly });
});

// Registro dos repositórios e UnitOfWork
builder.Services.AddScoped<ICondominioRepository, CondominioRepository>();
// Registre outros repositórios conforme necessário...
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Adiciona os controllers
builder.Services.AddControllers();

// Configura o Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HydroMeasure API v1");
        c.RoutePrefix = string.Empty; // Define que a swagger UI será exibida na raiz (https://localhost:7212/)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();