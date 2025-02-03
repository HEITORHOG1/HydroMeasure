using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Update
{
    public class UpdateLeituraCommand : IRequest<OperationResult<LeituraDto>>
    {
        public Guid Id { get; set; }
        public decimal LeituraAtual { get; set; }
        public DateTime DataLeitura { get; set; } = DateTime.UtcNow; // Data padrão

        public decimal Consumo { get; set; }
        // Você pode adicionar outras propriedades que podem ser atualizadas, se necessário
    }
}