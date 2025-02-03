using HydroMeasure.Domain.Entities;
using HydroMeasure.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HydroMeasure.Domain.Repositories
{
    public interface ILeituraRepository : IRepository<Leitura>
    {
        // Métodos específicos para Leitura, se necessário
        Task<Leitura?> GetLeituraComDetalhesAsync(Guid id); // Exemplo: Buscar Leitura com Hidrometro e Unidade
        Task<IEnumerable<Leitura>> GetLeiturasPorHidrometroAsync(Guid hidrometroId);
        Task<IEnumerable<Leitura>> GetLeiturasPorUnidadeAsync(Guid unidadeId);
        Task<Leitura?> GetUltimaLeituraPorHidrometroAsync(Guid hidrometroId); // Para calcular o consumo
    }
}
