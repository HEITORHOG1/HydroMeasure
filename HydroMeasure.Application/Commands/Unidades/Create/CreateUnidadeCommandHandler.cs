using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Enums;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Unidades.Create
{
    public class CreateUnidadeCommandHandler : IRequestHandler<CreateUnidadeCommand, OperationResult<UnidadeDto>>
    {
        private readonly IUnidadeRepository _unidadeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUnidadeCommandHandler(IUnidadeRepository unidadeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unidadeRepository = unidadeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<UnidadeDto>> Handle(CreateUnidadeCommand request, CancellationToken cancellationToken)
        {
            // 1. Converter TipoUnidade string para Enum
            if (!Enum.TryParse<TipoUnidade>(request.Tipo, true, out var tipoUnidade))
            {
                return new OperationResult<UnidadeDto>
                {
                    Success = false,
                    Message = "Tipo de unidade inválido.",
                    ErrorCode = "InvalidInput"
                };
            }

            // 2. Criar Entidade Unidade
            var unidade = new Unidade(
                request.CondominioId,
                request.Identificacao,
                tipoUnidade,
                request.MoradorResponsavel,
                request.MediaConsumo
            );

            // 3. Adicionar a Unidade ao Repositório
            await _unidadeRepository.AddAsync(unidade);

            // 4. Salvar as Alterações no Banco de Dados
            await _unitOfWork.SaveChangesAsync();

            // 5. Mapear a Entidade para DTO
            var unidadeDto = _mapper.Map<UnidadeDto>(unidade);

            // 6. Retornar Resultado de Sucesso
            return new OperationResult<UnidadeDto>
            {
                Success = true,
                Data = unidadeDto,
                Message = "Unidade criada com sucesso."
            };
        }
    }
}