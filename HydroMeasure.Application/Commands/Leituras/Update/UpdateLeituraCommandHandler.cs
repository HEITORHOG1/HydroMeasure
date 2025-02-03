using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Update
{
    public class UpdateLeituraCommandHandler : IRequestHandler<UpdateLeituraCommand, OperationResult<LeituraDto>>
    {
        private readonly ILeituraRepository _leituraRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeituraCommandHandler(ILeituraRepository leituraRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _leituraRepository = leituraRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<LeituraDto>> Handle(UpdateLeituraCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar a Leitura Existente
            var leitura = await _leituraRepository.GetByIdAsync(request.Id);
            if (leitura == null)
            {
                return new OperationResult<LeituraDto>
                {
                    Success = false,
                    Message = "Leitura não encontrada.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. **Usar o método Update da entidade:**
            leitura.Update(request.LeituraAtual, request.DataLeitura, request.Consumo, request.Id);

            // 3. Salvar Alterações
            await _unitOfWork.SaveChangesAsync();

            // 4. Mapear para DTO e Retornar
            var leituraDto = _mapper.Map<LeituraDto>(leitura);
            return new OperationResult<LeituraDto>
            {
                Success = true,
                Data = leituraDto,
                Message = "Leitura atualizada com sucesso."
            };
        }
    }
}
