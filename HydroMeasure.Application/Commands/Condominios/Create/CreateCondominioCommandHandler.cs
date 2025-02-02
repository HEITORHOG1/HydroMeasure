using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories;
using HydroMeasure.Domain.Repositories.Base;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Create
{
    public class CreateCondominioCommandHandler : IRequestHandler<CreateCondominioCommand, OperationResult<CondominioDto>>
    {
        private readonly ICondominioRepository _condominioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCondominioCommandHandler(ICondominioRepository condominioRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _condominioRepository = condominioRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult<CondominioDto>> Handle(CreateCondominioCommand request, CancellationToken cancellationToken)
        {
            var condominio = new Condominio(
                request.Nome,
                request.Endereco,
                request.CNPJ,
                request.Sindico,
                request.TelefoneSindico,
                request.EmailSindico
            );

            await _condominioRepository.AddAsync(condominio);
            await _unitOfWork.SaveChangesAsync();

            var condominioDto = _mapper.Map<CondominioDto>(condominio);

            return new OperationResult<CondominioDto>
            {
                Success = true,
                Data = condominioDto,
                Message = "Condomínio criado com sucesso."
            };
        }
    }
}