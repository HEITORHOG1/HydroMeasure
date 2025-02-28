using HydroMeasure.Hibrid.Shared.Model.Configuracao;

namespace HydroMeasure.Hibrid.Shared.Services
{
    public interface IConfiguracaoSistemaService
    {
        Task<ConfiguracaoSistemaDto?> GetConfiguracaoSistemaAsync();
        Task<OperationResult<ConfiguracaoSistemaDto>> CreateConfiguracaoSistemaAsync(CreateConfiguracaoSistemaCommand command);
        Task<OperationResult<ConfiguracaoSistemaDto>> UpdateConfiguracaoSistemaAsync(UpdateConfiguracaoSistemaCommand command);
        Task<OperationResult<bool>> ResetConfiguracaoSistemaAsync();
    }
}
