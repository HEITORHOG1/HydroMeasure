using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Common;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracaoSistema
{
    public class CreateConfiguracaoSistemaCommandHandler : IRequestHandler<CreateConfiguracaoSistemaCommand, OperationResult<ConfiguracaoSistemaDto>>
    {
        private readonly IConfiguracaoSistemaRepository _repository;

        public CreateConfiguracaoSistemaCommandHandler(IConfiguracaoSistemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> Handle(CreateConfiguracaoSistemaCommand request, CancellationToken cancellationToken)
        {
            var configuracao = new Domain.Entities.ConfiguracaoSistema
            {
                Idioma = request.Idioma,
                Tema = request.Tema,
                NotificacoesEmailHabilitadas = request.NotificacoesEmailHabilitadas,
                NotificacoesSmsHabilitadas = request.NotificacoesSmsHabilitadas,
                NotificacoesPushHabilitadas = request.NotificacoesPushHabilitadas,
                TentativasLoginMaximas = request.TentativasLoginMaximas,
                TempoBloqueioConta = request.TempoBloqueioConta,
                FormatoPadraoRelatorio = request.FormatoPadraoRelatorio,
                GerarRelatorioAutomatico = request.GerarRelatorioAutomatico,
                LimiteConsumoAlerta = request.LimiteConsumoAlerta,
                IntervaloAlertaConsumo = request.IntervaloAlertaConsumo,
                CorPrimariaPersonalizada = request.CorPrimariaPersonalizada,
                CorSecundariaPersonalizada = request.CorSecundariaPersonalizada,
                BackupAutomaticoHabilitado = request.BackupAutomaticoHabilitado,
                FrequenciaBackup = request.FrequenciaBackup
            };

            await _repository.AddAsync(configuracao);

            var dto = new ConfiguracaoSistemaDto
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

            return new OperationResult<ConfiguracaoSistemaDto>
            {
                Success = true,
                Data = dto,
                Message = "Configuração do sistema criada com sucesso."
            };
        }
    }
}