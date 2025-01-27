using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HydroMeasure.Infrastructure.Context
{
    public class HydroMeasureDbContextFactory : IDesignTimeDbContextFactory<HydroMeasureDbContext>
    {
        public HydroMeasureDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // Certifique-se de que o appsettings.json está na pasta do projeto Infrastructure ou ajuste o caminho
                .Build();

            var builder = new DbContextOptionsBuilder<HydroMeasureDbContext>();
            var connectionString = configuration.GetConnectionString("SqliteConnection"); // Use o nome da sua Connection String no appsettings.json

            builder.UseSqlite(connectionString); // Configure o provider SQLite (ou outro provider que você está usando)

            return new HydroMeasureDbContext(builder.Options);
        }
    }
}