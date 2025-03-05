using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Common;
using MediatR;

namespace HydroMeasure.Application.Commands.ConfiguracaoSistema
{
    public class CreateConfiguracaoSistemaCommand : IRequest<OperationResult<ConfiguracaoSistemaDto>>
    {
        public string Idioma { get; set; } = "pt-BR";
        public string Tema { get; set; } = "Padrão";

        public bool NotificacoesEmailHabilitadas { get; set; } = false;
        public bool NotificacoesSmsHabilitadas { get; set; } = false;
        public bool NotificacoesPushHabilitadas { get; set; } = false;

        public int TentativasLoginMaximas { get; set; } = 5;
        public int TempoBloqueioConta { get; set; } = 15;

        public string FormatoPadraoRelatorio { get; set; } = "PDF";
        public bool GerarRelatorioAutomatico { get; set; } = false;

        public decimal LimiteConsumoAlerta { get; set; } = 20;
        public int IntervaloAlertaConsumo { get; set; } = 30;

        public string CorPrimariaPersonalizada { get; set; } = "#512BD4";
        public string CorSecundariaPersonalizada { get; set; } = "#8A6FE8";

        public bool BackupAutomaticoHabilitado { get; set; } = false;
        public int FrequenciaBackup { get; set; } = 7;
    }

    public class UpdateConfiguracaoSistemaCommand : CreateConfiguracaoSistemaCommand
    {
        public Guid Id { get; set; }
    }

    public class ResetConfiguracaoSistemaCommand : IRequest<OperationResult<bool>>
    {
    }
}