using HydroMeasure.Domain.Common;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracaoSistema
{
    public class ResetConfiguracaoSistemaCommandHandler : IRequestHandler<ResetConfiguracaoSistemaCommand, OperationResult<bool>>
    {
        private readonly IConfiguracaoSistemaRepository _repository;

        public ResetConfiguracaoSistemaCommandHandler(IConfiguracaoSistemaRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult<bool>> Handle(ResetConfiguracaoSistemaCommand request, CancellationToken cancellationToken)
        {
            var configuracao = await _repository.GetAsync();

            if (configuracao == null)
            {
                return new OperationResult<bool>
                {
                    Success = false,
                    Message = "Configuração do sistema não encontrada."
                };
            }

            // Resetar para valores padrão
            configuracao.Idioma = "pt-BR";
            configuracao.Tema = "Padrão";
            configuracao.NotificacoesEmailHabilitadas = false;
            configuracao.NotificacoesSmsHabilitadas = false;
            configuracao.NotificacoesPushHabilitadas = false;
            configuracao.TentativasLoginMaximas = 5;
            configuracao.TempoBloqueioConta = 15;
            configuracao.FormatoPadraoRelatorio = "PDF";
            configuracao.GerarRelatorioAutomatico = false;
            configuracao.LimiteConsumoAlerta = 20;
            configuracao.IntervaloAlertaConsumo = 30;
            configuracao.CorPrimariaPersonalizada = "#512BD4";
            configuracao.CorSecundariaPersonalizada = "#8A6FE8";
            configuracao.BackupAutomaticoHabilitado = false;
            configuracao.FrequenciaBackup = 7;

            await _repository.UpdateAsync(configuracao);

            return new OperationResult<bool>
            {
                Success = true,
                Data = true,
                Message = "Configurações do sistema resetadas com sucesso."
            };
        }
    }
}