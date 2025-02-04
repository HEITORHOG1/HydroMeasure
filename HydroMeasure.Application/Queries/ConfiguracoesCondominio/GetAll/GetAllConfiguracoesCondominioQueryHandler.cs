using AutoMapper;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Domain.Repositories;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetAll
{
    public class GetAllConfiguracoesCondominioQueryHandler : IRequestHandler<GetAllConfiguracoesCondominioQuery, IEnumerable<ConfiguracaoCondominioDto>>
    {
        private readonly IConfiguracaoCondominioRepository _configuracaoCondominioRepository;
        private readonly IMapper _mapper;

        public GetAllConfiguracoesCondominioQueryHandler(IConfiguracaoCondominioRepository configuracaoCondominioRepository, IMapper mapper)
        {
            _configuracaoCondominioRepository = configuracaoCondominioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConfiguracaoCondominioDto>> Handle(GetAllConfiguracoesCondominioQuery request, CancellationToken cancellationToken)
        {
            var configuracoesCondominio = await _configuracaoCondominioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConfiguracaoCondominioDto>>(configuracoesCondominio);
        }
    }
}