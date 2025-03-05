using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Common;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracaoSistema
{
    public class UpdateConfiguracaoSistemaCommandHandler : IRequestHandler<UpdateConfiguracaoSistemaCommand, OperationResult<ConfiguracaoSistemaDto>>
    {
        private readonly IConfiguracaoSistemaRepository _repository;

        public UpdateConfiguracaoSistemaCommandHandler(IConfiguracaoSistemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<ConfiguracaoSistemaDto>> Handle(UpdateConfiguracaoSistemaCommand request, CancellationToken cancellationToken)
        {
            var configuracao = await _repository.GetByIdAsync(request.Id);

            if (configuracao == null)
            {
                return new OperationResult<ConfiguracaoSistemaDto>
                {
                    Success = false,
                    Message = "Configuração do sistema não encontrada."
                };
            }

            configuracao.Idioma = request.Idioma;
            configuracao.Tema = request.Tema;
            configuracao.NotificacoesEmailHabilitadas = request.NotificacoesEmailHabilitadas;
            configuracao.NotificacoesSmsHabilitadas = request.NotificacoesSmsHabilitadas;
            configuracao.NotificacoesPushHabilitadas = request.NotificacoesPushHabilitadas;
            configuracao.TentativasLoginMaximas = request.TentativasLoginMaximas;
            configuracao.TempoBloqueioConta = request.TempoBloqueioConta;
            configuracao.FormatoPadraoRelatorio = request.FormatoPadraoRelatorio;
            configuracao.GerarRelatorioAutomatico = request.GerarRelatorioAutomatico;
            configuracao.LimiteConsumoAlerta = request.LimiteConsumoAlerta;
            configuracao.IntervaloAlertaConsumo = request.IntervaloAlertaConsumo;
            configuracao.CorPrimariaPersonalizada = request.CorPrimariaPersonalizada;
            configuracao.CorSecundariaPersonalizada = request.CorSecundariaPersonalizada;
            configuracao.BackupAutomaticoHabilitado = request.BackupAutomaticoHabilitado;
            configuracao.FrequenciaBackup = request.FrequenciaBackup;

            await _repository.UpdateAsync(configuracao);

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
                Message = "Configuração do sistema atualizada com sucesso."
            };
        }
    }
}