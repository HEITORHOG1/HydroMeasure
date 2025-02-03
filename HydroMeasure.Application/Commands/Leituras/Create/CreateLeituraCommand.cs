using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Leituras.Create
{
    public class CreateLeituraCommand : IRequest<OperationResult<LeituraDto>>
    {
        public Guid HidrometroId { get; set; }
        public decimal LeituraAtual { get; set; }
        public DateTime DataLeitura { get; set; } = DateTime.UtcNow; // Data padrão para facilitar o uso na API
    }
}