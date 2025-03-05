using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HydroMeasure.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CampoNovoConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeSistema",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "VersaoSistema",
                table: "ConfiguracoesSistema");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "ConfiguracoesSistema",
                newName: "Tema");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "ConfiguracoesSistema",
                newName: "LimiteConsumoAlerta");

            migrationBuilder.AddColumn<bool>(
                name: "BackupAutomaticoHabilitado",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CorPrimariaPersonalizada",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorSecundariaPersonalizada",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FormatoPadraoRelatorio",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FrequenciaBackup",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "GerarRelatorioAutomatico",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Idioma",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IntervaloAlertaConsumo",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "NotificacoesEmailHabilitadas",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotificacoesPushHabilitadas",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotificacoesSmsHabilitadas",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TempoBloqueioConta",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TentativasLoginMaximas",
                table: "ConfiguracoesSistema",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackupAutomaticoHabilitado",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "CorPrimariaPersonalizada",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "CorSecundariaPersonalizada",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "FormatoPadraoRelatorio",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "FrequenciaBackup",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "GerarRelatorioAutomatico",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "Idioma",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "IntervaloAlertaConsumo",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "NotificacoesEmailHabilitadas",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "NotificacoesPushHabilitadas",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "NotificacoesSmsHabilitadas",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "TempoBloqueioConta",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "TentativasLoginMaximas",
                table: "ConfiguracoesSistema");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ConfiguracoesSistema");

            migrationBuilder.RenameColumn(
                name: "Tema",
                table: "ConfiguracoesSistema",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "LimiteConsumoAlerta",
                table: "ConfiguracoesSistema",
                newName: "DataAlteracao");

            migrationBuilder.AddColumn<string>(
                name: "NomeSistema",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VersaoSistema",
                table: "ConfiguracoesSistema",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}