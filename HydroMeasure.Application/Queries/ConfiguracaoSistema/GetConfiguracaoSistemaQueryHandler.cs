using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracaoSistema
{
    public class GetConfiguracaoSistemaQueryHandler : IRequestHandler<GetConfiguracaoSistemaQuery, ConfiguracaoSistemaDto?>
    {
        private readonly IConfiguracaoSistemaRepository _repository;

        public GetConfiguracaoSistemaQueryHandler(IConfiguracaoSistemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<ConfiguracaoSistemaDto?> Handle(GetConfiguracaoSistemaQuery request, CancellationToken cancellationToken)
        {
            var configuracao = await _repository.GetAsync();

            if (configuracao == null)
                return null;

            return new ConfiguracaoSistemaDto
            {
                Id = configuracao.Id,
                Idioma = configuracao.Idioma,
                Tema = configuracao.Tema,
                NotificacoesEmailHabilitadas = configuracao.NotificacoesEmailHabilitadas,
                NotificacoesSmsHabilitadas = configuracao.NotificacoesSmsHabilitadas,
                NotificacoesPushHabilitadas = configuracao.NotificacoesPushHabilitadas,
                TentativasLoginMaximas = configuracao.TentativasLoginMaximas,
                TempoBloqueioConta = configuracao.TempoBloqueioConta,
                FormatoPadraoRelatorio = configuracao.FormatoPadraoRelatorio,
                GerarRelatorioAutomatico = configuracao.GerarRelatorioAutomatico,
                LimiteConsumoAlerta = configuracao.LimiteConsumoAlerta,
                IntervaloAlertaConsumo = configuracao.IntervaloAlertaConsumo,
                CorPrimariaPersonalizada = configuracao.CorPrimariaPersonalizada,
                CorSecundariaPersonalizada = configuracao.CorSecundariaPersonalizada,
                BackupAutomaticoHabilitado = configuracao.BackupAutomaticoHabilitado,
                FrequenciaBackup = configuracao.FrequenciaBackup
            };
        }
    }
}