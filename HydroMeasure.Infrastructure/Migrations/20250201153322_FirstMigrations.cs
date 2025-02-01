using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HydroMeasure.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condominios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CNPJ = table.Column<string>(type: "TEXT", nullable: true),
                    Sindico = table.Column<string>(type: "TEXT", nullable: true),
                    TelefoneSindico = table.Column<string>(type: "TEXT", nullable: true),
                    EmailSindico = table.Column<string>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condominios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesSistema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeSistema = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    VersaoSistema = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesSistema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesCondominio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CondominioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MetodoCalculoMedia = table.Column<string>(type: "TEXT", nullable: false),
                    FormatoRelatorio = table.Column<string>(type: "TEXT", nullable: false),
                    PeriodicidadeAlerta = table.Column<string>(type: "TEXT", nullable: false),
                    ValorMetroCubico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesCondominio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesCondominio_Condominios_CondominioId",
                        column: x => x.CondominioId,
                        principalTable: "Condominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CondominioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Identificacao = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    MoradorResponsavel = table.Column<string>(type: "TEXT", nullable: true),
                    MediaConsumo = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unidades_Condominios_CondominioId",
                        column: x => x.CondominioId,
                        principalTable: "Condominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", nullable: false),
                    Perfil = table.Column<string>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CondominioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Condominios_CondominioId",
                        column: x => x.CondominioId,
                        principalTable: "Condominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hidrometros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumeroSerie = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", nullable: true),
                    DataInstalacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hidrometros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hidrometros_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alertas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Mensagem = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DataAlerta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Resolvido = table.Column<bool>(type: "INTEGER", nullable: false),
                    UsuarioResolvidoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CondominioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alertas_Condominios_CondominioId",
                        column: x => x.CondominioId,
                        principalTable: "Condominios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alertas_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alertas_Usuarios_UsuarioResolvidoId",
                        column: x => x.UsuarioResolvidoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    Periodo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Conteudo = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relatorios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leituras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    HidrometroId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LeituraAtual = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Consumo = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    LeituraAnteriorId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UnidadeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leituras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leituras_Hidrometros_HidrometroId",
                        column: x => x.HidrometroId,
                        principalTable: "Hidrometros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leituras_Leituras_LeituraAnteriorId",
                        column: x => x.LeituraAnteriorId,
                        principalTable: "Leituras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leituras_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_CondominioId",
                table: "Alertas",
                column: "CondominioId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UnidadeId",
                table: "Alertas",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Alertas_UsuarioResolvidoId",
                table: "Alertas",
                column: "UsuarioResolvidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesCondominio_CondominioId",
                table: "ConfiguracoesCondominio",
                column: "CondominioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hidrometros_NumeroSerie",
                table: "Hidrometros",
                column: "NumeroSerie",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hidrometros_UnidadeId",
                table: "Hidrometros",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_HidrometroId",
                table: "Leituras",
                column: "HidrometroId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_LeituraAnteriorId",
                table: "Leituras",
                column: "LeituraAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_Leituras_UnidadeId",
                table: "Leituras",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_UsuarioId",
                table: "Relatorios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Unidades_CondominioId",
                table: "Unidades",
                column: "CondominioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CondominioId",
                table: "Usuarios",
                column: "CondominioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alertas");

            migrationBuilder.DropTable(
                name: "ConfiguracoesCondominio");

            migrationBuilder.DropTable(
                name: "ConfiguracoesSistema");

            migrationBuilder.DropTable(
                name: "Leituras");

            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Hidrometros");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "Condominios");
        }
    }
}