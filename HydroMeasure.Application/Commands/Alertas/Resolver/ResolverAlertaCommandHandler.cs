using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Alertas.Resolver
{
    public class ResolverAlertaCommandHandler : IRequestHandler<ResolverAlertaCommand, OperationResult<AlertaDto>>
    {
        private readonly IAlertaRepository _alertaRepository;

        //private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ResolverAlertaCommandHandler(
            IAlertaRepository alertaRepository,
            //IUsuarioRepository usuarioRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _alertaRepository = alertaRepository;
            //_usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<AlertaDto>> Handle(ResolverAlertaCommand request, CancellationToken cancellationToken)
        {
            // 1. Validar se o Alerta existe
            var alerta = await _alertaRepository.GetByIdAsync(request.Id);
            if (alerta == null)
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "Alerta não encontrado.",
                    ErrorCode = "NotFound"
                };
            }

            // 2. Validar se o Alerta já foi resolvido
            if (alerta.Resolvido)
            {
                return new OperationResult<AlertaDto>
                {
                    Success = false,
                    Message = "Alerta já foi resolvido.",
                    ErrorCode = "InvalidOperation"
                };
            }

            // 3. Validar se o Usuário existe
            //var usuario = await _usuarioRepository.GetByIdAsync(request.UsuarioResolvidoId);
            //if (usuario == null)
            //{
            //    return new OperationResult<AlertaDto>
            //    {
            //        Success = false,
            //        Message = "Usuário não encontrado.",
            //        ErrorCode = "NotFound"
            //    };
            //}

            // 4. Atualizar o Alerta como resolvido
            alerta.Update(
                alerta.UnidadeId,
                alerta.Tipo,
                alerta.Mensagem,
                alerta.DataAlerta,
                true, // Resolvido
                request.UsuarioResolvidoId // Usuário que resolveu
            );

            // 5. Salvar alterações
            await _unitOfWork.SaveChangesAsync();

            // 6. Mapear para DTO e retornar
            var alertaDto = _mapper.Map<AlertaDto>(alerta);

            return new OperationResult<AlertaDto>
            {
                Success = true,
                Data = alertaDto,
                Message = "Alerta resolvido com sucesso."
            };
        }
    }
}