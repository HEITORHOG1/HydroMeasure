using HydroMeasure.Application.Commands.Alertas.Create;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HydroMeasure.Application.Services
{
    public class AlertaGenerationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AlertaGenerationService> _logger;

        public AlertaGenerationService(
            IServiceProvider serviceProvider,
            ILogger<AlertaGenerationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Checking consumption for alerts at: {time}", DateTimeOffset.Now);

                try
                {
                    await CheckConsumoAlerts();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in alert generation service");
                }

                // Run every 6 hours
                await Task.Delay(TimeSpan.FromHours(6), stoppingToken);
            }
        }

        private async Task CheckConsumoAlerts()
        {
            using var scope = _serviceProvider.CreateScope();

            var unidadeRepository = scope.ServiceProvider.GetRequiredService<IUnidadeRepository>();
            var leituraRepository = scope.ServiceProvider.GetRequiredService<ILeituraRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var unidades = await unidadeRepository.GetAllAsync();

            foreach (var unidade in unidades)
            {
                var ultimasLeituras = await leituraRepository.GetLeiturasPorUnidadeAsync(unidade.Id);
                var ultimaLeitura = ultimasLeituras.OrderByDescending(l => l.DataLeitura).FirstOrDefault();

                if (ultimaLeitura != null)
                {
                    // Check if consumption is above average
                    if (ultimaLeitura.Consumo > unidade.MediaConsumo * 1.5m)
                    {
                        var alertaCommand = new CreateAlertaCommand
                        {
                            UnidadeId = unidade.Id,
                            CondominioId = unidade.CondominioId,
                            Tipo = TipoAlerta.ConsumoAcimaMedia.ToString(),
                            Mensagem = $"Consumo de água excessivo: {ultimaLeitura.Consumo:N2} m³ (Média: {unidade.MediaConsumo:N2} m³)"
                        };

                        await mediator.Send(alertaCommand);
                    }
                }
            }
        }
    }
}