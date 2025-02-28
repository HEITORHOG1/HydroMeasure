namespace HydroMeasure.Hibrid.Shared.Model.Configuracao
{
    public class ConfiguracaoSistemaDto
    {
        public Guid Id { get; set; }

        // Configurações gerais do sistema
        public string Idioma { get; set; } = "pt-BR";

        public string Tema { get; set; } = "Padrão";

        // Configurações de notificação
        public bool NotificacoesEmailHabilitadas { get; set; } = false;

        public bool NotificacoesSmsHabilitadas { get; set; } = false;
        public bool NotificacoesPushHabilitadas { get; set; } = false;

        // Configurações de segurança
        public int TentativasLoginMaximas { get; set; } = 5;

        public int TempoBloqueioConta { get; set; } = 15; // Minutos

        // Configurações de relatórios
        public string FormatoPadraoRelatorio { get; set; } = "PDF";

        public bool GerarRelatorioAutomatico { get; set; } = false;

        // Configurações de medição
        public decimal LimiteConsumoAlerta { get; set; } = 20; // m³

        public int IntervaloAlertaConsumo { get; set; } = 30; // Dias

        // Personalizações visuais
        public string CorPrimariaPersonalizada { get; set; } = "#512BD4";

        public string CorSecundariaPersonalizada { get; set; } = "#8A6FE8";

        // Backup e sincronização
        public bool BackupAutomaticoHabilitado { get; set; } = false;

        public int FrequenciaBackup { get; set; } = 7; // Dias
    }
}