using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;
using System.Text.Json;

namespace HydroMeasure.Application.Commands.Relatorios.Create
{
    public class GerarRelatorioCommandHandler : IRequestHandler<GerarRelatorioCommand, OperationResult<RelatorioDto>>
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly ILeituraRepository _leituraRepository;

        //private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public GerarRelatorioCommandHandler(
            IRelatorioRepository relatorioRepository,
            ICondominioRepository condominioRepository,
            IUnidadeRepository unidadeRepository,
            ILeituraRepository leituraRepository,
            //IUsuarioRepository usuarioRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _relatorioRepository = relatorioRepository;
            _condominioRepository = condominioRepository;
            _unidadeRepository = unidadeRepository;
            _leituraRepository = leituraRepository;
            //_usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<RelatorioDto>> Handle(GerarRelatorioCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar se o Condominio existe
            var condominio = await _condominioRepository.GetByIdAsync(request.CondominioId);
            if (condominio == null)
            {
                return new OperationResult<RelatorioDto>
                {
                    Success = false,
                    Message = "Condomínio não encontrado.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validar se o Usuário existe
            //var usuario = await _usuarioRepository.GetByIdAsync(request.UsuarioId);
            //if (usuario == null)
            //{
            //    return new OperationResult<RelatorioDto>
            //    {
            //        Success = false,
            //        Message = "Usuário não encontrado.",
            //        ErrorCode = "NotFound"
            //    };
            //}

            // 3. Validar e converter TipoRelatorio string para Enum
            if (!Enum.TryParse<TipoRelatorio>(request.Tipo, true, out var tipoRelatorio))
            {
                return new OperationResult<RelatorioDto>
                {
                    Success = false,
                    Message = "Tipo de relatório inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 4. Gerar dados do relatório com base no tipo
            var dadosRelatorio = await GerarDadosRelatorio(tipoRelatorio, request.CondominioId, request.UnidadeId, request.Periodo);

            // 5. Criar entidade Relatorio
            var relatorio = new Relatorio(
                tipoRelatorio,
                request.Periodo,
                JsonSerializer.Serialize(dadosRelatorio),
                request.UsuarioId
            );

            // 6. Salvar relatório no repositório
            await _relatorioRepository.AddAsync(relatorio);
            await _unitOfWork.SaveChangesAsync();

            // 7. Mapear para DTO e retornar
            var relatorioDto = _mapper.Map<RelatorioDto>(relatorio);

            return new OperationResult<RelatorioDto>
            {
                Success = true,
                Data = relatorioDto,
                Message = "Relatório gerado com sucesso."
            };
        }

        private async Task<object> GerarDadosRelatorio(TipoRelatorio tipo, Guid condominioId, Guid? unidadeId, string periodo)
        {
            // Lógica para gerar diferentes tipos de relatórios
            switch (tipo)
            {
                case TipoRelatorio.GeralCondominio:
                    return await GerarRelatorioCcondominio(condominioId, periodo);

                case TipoRelatorio.IndividualUnidade:
                    if (unidadeId == null)
                        throw new ArgumentException("ID da unidade é obrigatório para relatório individual.");
                    return await GerarRelatorioUnidade(unidadeId.Value, periodo);

                case TipoRelatorio.RankingConsumo:
                    return await GerarRelatorioRankingConsumo(condominioId, periodo);

                default:
                    throw new NotImplementedException($"Tipo de relatório {tipo} não implementado.");
            }
        }

        private async Task<object> GerarRelatorioCcondominio(Guid condominioId, string periodo)
        {
            // Buscar todas as unidades do condomínio
            var unidades = await _unidadeRepository.FindAsync(u => u.CondominioId == condominioId);

            // Extrair ano e mês ou período do formato (YYYY-MM, YYYY-Q#, YYYY-S#)
            DateTime inicio, fim;
            ParsePeriodo(periodo, out inicio, out fim);

            var resumoUnidades = new List<object>();
            decimal consumoTotal = 0;

            foreach (var unidade in unidades)
            {
                // Buscar todas as leituras da unidade no período
                var leituras = await _leituraRepository.GetLeiturasPorUnidadeAsync(unidade.Id);
                var leiturasNoPeriodo = leituras.Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim).ToList();

                if (leiturasNoPeriodo.Any())
                {
                    var consumoUnidade = leiturasNoPeriodo.Sum(l => l.Consumo);
                    consumoTotal += consumoUnidade;

                    resumoUnidades.Add(new
                    {
                        UnidadeId = unidade.Id,
                        Identificacao = unidade.Identificacao,
                        Tipo = unidade.Tipo.ToString(),
                        ConsumoTotal = consumoUnidade,
                        UltimaLeitura = leiturasNoPeriodo.OrderByDescending(l => l.DataLeitura).FirstOrDefault()?.LeituraAtual ?? 0
                    });
                }
            }

            return new
            {
                Periodo = periodo,
                DataInicio = inicio,
                DataFim = fim,
                ConsumoTotal = consumoTotal,
                QuantidadeUnidades = resumoUnidades.Count,
                Unidades = resumoUnidades
            };
        }

        private async Task<object> GerarRelatorioUnidade(Guid unidadeId, string periodo)
        {
            // Buscar a unidade
            var unidade = await _unidadeRepository.GetByIdAsync(unidadeId);
            if (unidade == null)
                throw new Exception("Unidade não encontrada.");

            // Extrair ano e mês ou período do formato (YYYY-MM, YYYY-Q#, YYYY-S#)
            DateTime inicio, fim;
            ParsePeriodo(periodo, out inicio, out fim);

            // Buscar leituras da unidade no período
            var leituras = await _leituraRepository.GetLeiturasPorUnidadeAsync(unidadeId);
            var leiturasNoPeriodo = leituras
                .Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim)
                .OrderBy(l => l.DataLeitura)
                .ToList();

            var consumoTotal = leiturasNoPeriodo.Sum(l => l.Consumo);
            var mediaConsumo = leiturasNoPeriodo.Any() ? consumoTotal / leiturasNoPeriodo.Count : 0;

            return new
            {
                Periodo = periodo,
                DataInicio = inicio,
                DataFim = fim,
                UnidadeId = unidade.Id,
                CondominioId = unidade.CondominioId,
                Identificacao = unidade.Identificacao,
                Tipo = unidade.Tipo.ToString(),
                MoradorResponsavel = unidade.MoradorResponsavel,
                QuantidadeLeituras = leiturasNoPeriodo.Count,
                ConsumoTotal = consumoTotal,
                MediaConsumo = mediaConsumo,
                Leituras = leiturasNoPeriodo.Select(l => new
                {
                    Id = l.Id,
                    Data = l.DataLeitura,
                    LeituraAtual = l.LeituraAtual,
                    Consumo = l.Consumo
                }).ToList()
            };
        }

        private async Task<object> GerarRelatorioRankingConsumo(Guid condominioId, string periodo)
        {
            // Buscar todas as unidades do condomínio
            var unidades = await _unidadeRepository.FindAsync(u => u.CondominioId == condominioId);

            // Extrair ano e mês ou período do formato (YYYY-MM, YYYY-Q#, YYYY-S#)
            DateTime inicio, fim;
            ParsePeriodo(periodo, out inicio, out fim);

            var rankingUnidades = new List<object>();

            foreach (var unidade in unidades)
            {
                // Buscar todas as leituras da unidade no período
                var leituras = await _leituraRepository.GetLeiturasPorUnidadeAsync(unidade.Id);
                var leiturasNoPeriodo = leituras.Where(l => l.DataLeitura >= inicio && l.DataLeitura <= fim).ToList();

                if (leiturasNoPeriodo.Any())
                {
                    var consumoUnidade = leiturasNoPeriodo.Sum(l => l.Consumo);

                    rankingUnidades.Add(new
                    {
                        UnidadeId = unidade.Id,
                        Identificacao = unidade.Identificacao,
                        Tipo = unidade.Tipo.ToString(),
                        MoradorResponsavel = unidade.MoradorResponsavel,
                        ConsumoTotal = consumoUnidade
                    });
                }
            }

            // Ordenar o ranking por consumo (maior para menor)
            var ranking = rankingUnidades
                .OrderByDescending(u => ((dynamic)u).ConsumoTotal)
                .ToList();

            return new
            {
                Periodo = periodo,
                DataInicio = inicio,
                DataFim = fim,
                QuantidadeUnidades = ranking.Count,
                Ranking = ranking
            };
        }

        private void ParsePeriodo(string periodo, out DateTime inicio, out DateTime fim)
        {
            // Formato: YYYY-MM (mensal), YYYY-Q# (trimestral), YYYY-S# (semestral)
            var parts = periodo.Split('-');
            var ano = int.Parse(parts[0]);

            if (parts[1].StartsWith("Q")) // Trimestral (Quarter)
            {
                var trimestre = int.Parse(parts[1].Substring(1));
                int mes = (trimestre - 1) * 3 + 1;
                inicio = new DateTime(ano, mes, 1);
                fim = inicio.AddMonths(3).AddDays(-1);
            }
            else if (parts[1].StartsWith("S")) // Semestral
            {
                var semestre = int.Parse(parts[1].Substring(1));
                int mes = (semestre - 1) * 6 + 1;
                inicio = new DateTime(ano, mes, 1);
                fim = inicio.AddMonths(6).AddDays(-1);
            }
            else // Mensal
            {
                var mes = int.Parse(parts[1]);
                inicio = new DateTime(ano, mes, 1);
                fim = inicio.AddMonths(1).AddDays(-1);
            }
        }
    }
}