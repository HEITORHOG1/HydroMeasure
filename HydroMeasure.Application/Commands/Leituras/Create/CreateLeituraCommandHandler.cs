using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Create
{
    public class CreateLeituraCommandHandler : IRequestHandler<CreateLeituraCommand, OperationResult<LeituraDto>>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IHidrometroRepository _hidrometroRepository; // Precisa para validar HidrometroId
        private readonly IUnidadeRepository _unidadeRepository; // Precisa para validar UnidadeId
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeituraCommandHandler(ILeituraRepository leituraRepository, IHidrometroRepository hidrometroRepository, IUnidadeRepository unidadeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _leituraRepository = leituraRepository;
            _hidrometroRepository = hidrometroRepository;
            _unidadeRepository = unidadeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<LeituraDto>> Handle(CreateLeituraCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar HidrometroId e se o Hidrometro existe
            var hidrometro = await _hidrometroRepository.GetByIdAsync(request.HidrometroId);
            if (hidrometro == null)
            {
                return new OperationResult<LeituraDto> // Criação direta do OperationResult
                {
                    Success = false,
                    Message = "Hidrometro não encontrado.",
                    ErrorCode = "NotFound"
                };
            }
            // 2. Validar UnidadeId e se a Unidade existe (pode pegar da Unidade do Hidrometro ou receber UnidadeId no Command se precisar)
            var unidade = await _unidadeRepository.GetByIdAsync(hidrometro.UnidadeId); // Assumindo que pega da Unidade do Hidrometro
            if (unidade == null)
            {
                return new OperationResult<LeituraDto> // Criação direta do OperationResult
                {
                    Success = false,
                    Message = "Unidade não encontrada para o Hidrometro.",
                    ErrorCode = "NotFound"
                };
            }

            // 3. Obter a leitura anterior para calcular o consumo
            var ultimaLeitura = await _leituraRepository.GetUltimaLeituraPorHidrometroAsync(request.HidrometroId);
            decimal consumo = 0;
            if (ultimaLeitura != null)
            {
                consumo = request.LeituraAtual - ultimaLeitura.LeituraAtual;
                if (consumo < 0) consumo = 0; // Consumo não pode ser negativo (pode ter lógica mais complexa aqui)
            }

            // 4. Criar Entidade Leitura
            var leitura = new Leitura(
                request.HidrometroId,
                unidade.Id, // Usar UnidadeId da Unidade relacionada ao Hidrometro
                request.LeituraAtual,
                request.DataLeitura,
                consumo,
                ultimaLeitura?.Id // LeituraAnteriorId
            );

            // 5. Adicionar a Leitura ao Repositório
            await _leituraRepository.AddAsync(leitura);

            // 6. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 7. Mapear a Entidade para DTO
            var leituraDto = _mapper.Map<LeituraDto>(leitura);

            // 8. Retornar Resultado de Sucesso
            return new OperationResult<LeituraDto> // Criação direta do OperationResult
            {
                Success = true,
                Data = leituraDto,
                Message = "Leitura registrada com sucesso."
            };
        }
    }
}