using HydroMeasure.Application.DTOs;
using MediatR;

namespace HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetAll
{
    public class GetAllConfiguracoesCondominioQuery : IRequest<IEnumerable<ConfiguracaoCondominioDto>>
    {
        // Esta query não precisa de parâmetros, pois busca todas as configurações.
    }
}