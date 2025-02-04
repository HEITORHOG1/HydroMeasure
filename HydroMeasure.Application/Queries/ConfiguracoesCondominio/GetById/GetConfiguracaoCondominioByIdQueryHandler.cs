using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetById
{
    public class GetConfiguracaoCondominioByIdQueryHandler : IRequestHandler<GetConfiguracaoCondominioByIdQuery, ConfiguracaoCondominioDto?>
    {
        private readonly IConfiguracaoCondominioRepository _configuracaoCondominioRepository;
        private readonly IMapper _mapper;

        public GetConfiguracaoCondominioByIdQueryHandler(IConfiguracaoCondominioRepository configuracaoCondominioRepository, IMapper mapper)
        {
            _configuracaoCondominioRepository = configuracaoCondominioRepository;
            _mapper = mapper;
        }

        public async Task<ConfiguracaoCondominioDto?> Handle(GetConfiguracaoCondominioByIdQuery request, CancellationToken cancellationToken)
        {
            var configuracaoCondominio = await _configuracaoCondominioRepository.GetByIdAsync(request.Id);
            return configuracaoCondominio != null ? _mapper.Map<ConfiguracaoCondominioDto>(configuracaoCondominio) : null;
        }
    }
}