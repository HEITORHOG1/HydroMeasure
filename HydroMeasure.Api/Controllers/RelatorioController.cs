using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Relatorios.Create;
using HydroMeasure.Application.Queries.Relatorios.GetByCondominio;
using HydroMeasure.Application.Queries.Relatorios.GetById;
using HydroMeasure.Application.Queries.Relatorios.GetByPeriodo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RelatorioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GerarRelatorio([FromBody] GerarRelatorioCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetRelatorioByIdQuery(id);
            var relatorio = await _mediator.Send(query);
            if (relatorio == null)
                return NotFound();
            return Ok(relatorio);
        }

        [HttpGet("condominio/{condominioId}")]
        public async Task<IActionResult> GetByCondominio(Guid condominioId)
        {
            var query = new GetRelatoriosByCondominioQuery(condominioId);
            var relatorios = await _mediator.Send(query);
            return Ok(relatorios);
        }

        [HttpGet("periodo/{periodo}")]
        public async Task<IActionResult> GetByPeriodo(string periodo)
        {
            var query = new GetRelatoriosByPeriodoQuery(periodo);
            var relatorios = await _mediator.Send(query);
            return Ok(relatorios);
        }
    }
}