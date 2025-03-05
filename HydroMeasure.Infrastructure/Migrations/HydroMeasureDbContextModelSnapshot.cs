﻿// <auto-generated />
using System;
using HydroMeasure.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HydroMeasure.Infrastructure.Migrations
{
    [DbContext(typeof(HydroMeasureDbContext))]
    partial class HydroMeasureDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Alerta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CondominioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlerta")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mensagem")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Resolvido")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UnidadeId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UsuarioResolvidoId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CondominioId");

                    b.HasIndex("UnidadeId");

                    b.HasIndex("UsuarioResolvidoId");

                    b.ToTable("Alertas", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Condominio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailSindico")
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Sindico")
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefoneSindico")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Condominios", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.ConfiguracaoCondominio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CondominioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("FormatoRelatorio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MetodoCalculoMedia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PeriodicidadeAlerta")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("ValorMetroCubico")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CondominioId")
                        .IsUnique();

                    b.ToTable("ConfiguracoesCondominio", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.ConfiguracaoSistema", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("BackupAutomaticoHabilitado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CorPrimariaPersonalizada")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CorSecundariaPersonalizada")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("FormatoPadraoRelatorio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FrequenciaBackup")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GerarRelatorioAutomatico")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Idioma")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<int>("IntervaloAlertaConsumo")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("LimiteConsumoAlerta")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NotificacoesEmailHabilitadas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotificacoesPushHabilitadas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("NotificacoesSmsHabilitadas")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tema")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TempoBloqueioConta")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TentativasLoginMaximas")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ConfiguracoesSistema", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Hidrometro", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataInstalacao")
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroSerie")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UnidadeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NumeroSerie")
                        .IsUnique();

                    b.HasIndex("UnidadeId");

                    b.ToTable("Hidrometros", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Leitura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Consumo")
                        .HasColumnType("decimal(18,3)");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataLeitura")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HidrometroId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("LeituraAnteriorId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("LeituraAtual")
                        .HasColumnType("decimal(18,3)");

                    b.Property<Guid>("UnidadeId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("HidrometroId");

                    b.HasIndex("LeituraAnteriorId");

                    b.HasIndex("UnidadeId");

                    b.ToTable("Leituras", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Relatorio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Relatorios", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Unidade", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CondominioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Identificacao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("MediaConsumo")
                        .HasColumnType("decimal(18,3)");

                    b.Property<string>("MoradorResponsavel")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CondominioId");

                    b.ToTable("Unidades", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("CondominioId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAlteracao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Perfil")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CondominioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Alerta", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Condominio", "Condominio")
                        .WithMany("Alertas")
                        .HasForeignKey("CondominioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HydroMeasure.Domain.Entities.Unidade", "Unidade")
                        .WithMany("Alertas")
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HydroMeasure.Domain.Entities.Usuario", "UsuarioResolvido")
                        .WithMany("AlertasResolvidos")
                        .HasForeignKey("UsuarioResolvidoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Condominio");

                    b.Navigation("Unidade");

                    b.Navigation("UsuarioResolvido");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.ConfiguracaoCondominio", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Condominio", "Condominio")
                        .WithOne("Configuracao")
                        .HasForeignKey("HydroMeasure.Domain.Entities.ConfiguracaoCondominio", "CondominioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condominio");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Hidrometro", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Unidade", "Unidade")
                        .WithMany("Hidrometros")
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unidade");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Leitura", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Hidrometro", "Hidrometro")
                        .WithMany("Leituras")
                        .HasForeignKey("HidrometroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HydroMeasure.Domain.Entities.Leitura", "LeituraAnterior")
                        .WithMany()
                        .HasForeignKey("LeituraAnteriorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HydroMeasure.Domain.Entities.Unidade", "Unidade")
                        .WithMany("Leituras")
                        .HasForeignKey("UnidadeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hidrometro");

                    b.Navigation("LeituraAnterior");

                    b.Navigation("Unidade");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Relatorio", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Usuario", "Usuario")
                        .WithMany("RelatoriosGerados")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Unidade", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Condominio", "Condominio")
                        .WithMany("Unidades")
                        .HasForeignKey("CondominioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condominio");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("HydroMeasure.Domain.Entities.Condominio", "Condominio")
                        .WithMany("Usuarios")
                        .HasForeignKey("CondominioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Condominio");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Condominio", b =>
                {
                    b.Navigation("Alertas");

                    b.Navigation("Configuracao")
                        .IsRequired();

                    b.Navigation("Unidades");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Hidrometro", b =>
                {
                    b.Navigation("Leituras");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Unidade", b =>
                {
                    b.Navigation("Alertas");

                    b.Navigation("Hidrometros");

                    b.Navigation("Leituras");
                });

            modelBuilder.Entity("HydroMeasure.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("AlertasResolvidos");

                    b.Navigation("RelatoriosGerados");
                });
#pragma warning restore 612, 618
        }
    }
}
