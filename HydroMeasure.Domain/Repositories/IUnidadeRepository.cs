using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories.Base;

namespace HydroMeasure.Domain.Repositories
{
    public interface IUnidadeRepository : IRepository<Unidade>
    {
        // Métodos específicos de Unidade, se necessário (por enquanto, pode ser vazio)
        Task<Unidade?> GetUnidadeWithDetailsAsync(Guid id); // Exemplo: Se precisar buscar Unidade com Hidrometros/Leituras
    }
}