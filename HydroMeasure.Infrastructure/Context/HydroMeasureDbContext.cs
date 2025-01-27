using HydroMeasure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HydroMeasure.Infrastructure.Context
{
    public class HydroMeasureDbContext : DbContext
    {
        public HydroMeasureDbContext(DbContextOptions<HydroMeasureDbContext> options) : base(options)
        {
        }

        public DbSet<Condominio> Condominios { get; set; }
        public DbSet<ConfiguracaoCondominio> ConfiguracoesCondominio { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Hidrometro> Hidrometros { get; set; }
        public DbSet<Leitura> Leituras { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Alerta> Alertas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ConfiguracaoSistema> ConfiguracoesSistema { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurações Fluent API para as entidades (serão adicionadas nos próximos passos)
            ConfigureCondominio(modelBuilder);
            ConfigureConfiguracaoCondominio(modelBuilder);
            ConfigureUnidade(modelBuilder);
            ConfigureHidrometro(modelBuilder);
            ConfigureLeitura(modelBuilder);
            ConfigureRelatorio(modelBuilder);
            ConfigureAlerta(modelBuilder);
            ConfigureUsuario(modelBuilder);
            ConfigureConfiguracaoSistema(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureCondominio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Condominio>(entity =>
            {
                entity.ToTable("Condominios");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.DataCadastro)
                    .IsRequired();

                entity.Property(e => e.DataAlteracao)
                    .IsRequired();

                entity.Property(e => e.Ativo)
                    .IsRequired();

                entity.HasOne(e => e.Configuracao)
                    .WithOne(c => c.Condominio)
                    .HasForeignKey<ConfiguracaoCondominio>(c => c.CondominioId);

                entity.HasMany(e => e.Unidades)
                    .WithOne(u => u.Condominio)
                    .HasForeignKey(u => u.CondominioId)
                    .OnDelete(DeleteBehavior.Cascade); // Adicionado Cascade Delete para Unidades

                entity.HasMany(e => e.Usuarios)
                   .WithOne(u => u.Condominio)
                   .HasForeignKey(u => u.CondominioId)
                   .OnDelete(DeleteBehavior.Cascade); // Adicionado Cascade Delete para Usuarios

                entity.HasMany(e => e.Alertas)
                   .WithOne(a => a.Condominio)
                   .HasForeignKey(a => a.CondominioId)
                   .OnDelete(DeleteBehavior.Cascade); // Adicionado Cascade Delete para Alertas
            });
        }

        private void ConfigureConfiguracaoCondominio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfiguracaoCondominio>(entity =>
            {
                entity.ToTable("ConfiguracoesCondominio");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CondominioId)
                    .IsRequired();

                entity.Property(e => e.MetodoCalculoMedia)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.FormatoRelatorio)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.PeriodicidadeAlerta)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.ValorMetroCubico)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasIndex(e => e.CondominioId)
                    .IsUnique(); // Garante que só existe uma Configuração por Condominio
            });
        }

        private void ConfigureUnidade(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.ToTable("Unidades");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CondominioId)
                    .IsRequired();

                entity.Property(e => e.Identificacao)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.MediaConsumo)
                    .IsRequired()
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.Ativo)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasOne(e => e.Condominio)
                    .WithMany(c => c.Unidades)
                    .HasForeignKey(e => e.CondominioId);

                entity.HasMany(e => e.Hidrometros)
                    .WithOne(h => h.Unidade)
                    .HasForeignKey(h => h.UnidadeId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete para Hidrometros

                entity.HasMany(e => e.Leituras)
                    .WithOne(l => l.Unidade) // Corrigido para Unidade em vez de Hidrometro
                    .HasForeignKey(l => l.UnidadeId) // Corrigido FK para UnidadeId
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete para Leituras

                entity.HasMany(e => e.Alertas)
                  .WithOne(a => a.Unidade)
                  .HasForeignKey(a => a.UnidadeId)
                  .OnDelete(DeleteBehavior.Cascade); // Adicionado Cascade Delete para Alertas
            });
        }

        private void ConfigureHidrometro(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hidrometro>(entity =>
            {
                entity.ToTable("Hidrometros");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UnidadeId)
                    .IsRequired();

                entity.Property(e => e.NumeroSerie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(e => e.NumeroSerie)
                    .IsUnique(); // Garante que NumeroSerie seja único

                entity.Property(e => e.Ativo)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasOne(e => e.Unidade)
                    .WithMany(u => u.Hidrometros)
                    .HasForeignKey(e => e.UnidadeId);

                entity.HasMany(e => e.Leituras)
                    .WithOne(l => l.Hidrometro)
                    .HasForeignKey(e => e.HidrometroId)
                    .OnDelete(DeleteBehavior.Cascade); // Cascade delete para Leituras
            });
        }

        private void ConfigureLeitura(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leitura>(entity =>
            {
                entity.ToTable("Leituras");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.HidrometroId)
                    .IsRequired();

                entity.Property(e => e.LeituraAtual)
                    .IsRequired()
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.DataLeitura)
                    .IsRequired();

                entity.Property(e => e.Consumo)
                    .IsRequired()
                    .HasColumnType("decimal(18,3)");

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasOne(e => e.Hidrometro)
                    .WithMany(h => h.Leituras)
                    .HasForeignKey(e => e.HidrometroId);

                entity.HasOne(e => e.LeituraAnterior)
                    .WithMany()
                    .HasForeignKey(e => e.LeituraAnteriorId)
                    .OnDelete(DeleteBehavior.Restrict); // Restrict delete para evitar deleção em cascata indesejada

                entity.HasOne(e => e.Unidade)
                  .WithMany(u => u.Leituras)
                  .HasForeignKey(e => e.UnidadeId);
            });
        }

        private void ConfigureRelatorio(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Relatorio>(entity =>
            {
                entity.ToTable("Relatorios");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.Periodo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Conteudo)
                    .IsRequired(); // Sem limite de tamanho - considerar tipo apropriado para grandes volumes (ex: ntext no SQL Server, ou armazenar em arquivo e referenciar o caminho)

                entity.Property(e => e.UsuarioId)
                    .IsRequired();

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasOne(e => e.Usuario)
                    .WithMany(u => u.RelatoriosGerados)
                    .HasForeignKey(e => e.UsuarioId);
            });
        }

        private void ConfigureAlerta(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alerta>(entity =>
            {
                entity.ToTable("Alertas");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UnidadeId)
                    .IsRequired();

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.Mensagem)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Resolvido)
                    .IsRequired();

                entity.Property(e => e.DataAlerta)
                    .IsRequired();

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasOne(e => e.Unidade)
                    .WithMany(u => u.Alertas)
                    .HasForeignKey(e => e.UnidadeId);

                entity.HasOne(e => e.UsuarioResolvido)
                    .WithMany(u => u.AlertasResolvidos)
                    .HasForeignKey(e => e.UsuarioResolvidoId)
                    .IsRequired(false) // UsuarioResolvidoId é opcional
                    .OnDelete(DeleteBehavior.Restrict); // Restrict delete para evitar deleção em cascata indesejada

                entity.HasOne(e => e.Condominio)
                   .WithMany(c => c.Alertas)
                   .HasForeignKey(e => e.CondominioId);
            });
        }

        private void ConfigureUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasIndex(e => e.Email)
                    .IsUnique(); // Garante que Email seja único

                entity.Property(e => e.Senha)
                    .IsRequired(); // Senha será armazenada HASHED - não limitar tamanho aqui

                entity.Property(e => e.Perfil)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(e => e.Ativo)
                    .IsRequired();

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();

                entity.HasMany(e => e.RelatoriosGerados)
                    .WithOne(r => r.Usuario)
                    .HasForeignKey(r => r.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict); // Restrict delete para evitar deleção em cascata indesejada

                entity.HasMany(e => e.AlertasResolvidos)
                    .WithOne(a => a.UsuarioResolvido)
                    .HasForeignKey(a => a.UsuarioResolvidoId)
                    .OnDelete(DeleteBehavior.Restrict); // Restrict delete para evitar deleção em cascata indesejada]]

                // Configuração do relacionamento com Condominio (N-1)
                entity.HasOne(e => e.Condominio)
                    .WithMany(c => c.Usuarios)
                    .HasForeignKey(e => e.CondominioId);
            });
        }

        private void ConfigureConfiguracaoSistema(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfiguracaoSistema>(entity =>
            {
                entity.ToTable("ConfiguracoesSistema");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.NomeSistema)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.VersaoSistema)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DataCadastro)
                    .IsRequired();
                entity.Property(e => e.DataAlteracao)
                   .IsRequired();
            });
        }
    }
}