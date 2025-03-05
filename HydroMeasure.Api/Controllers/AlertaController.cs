using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Alertas.Create;
using HydroMeasure.Application.Commands.Alertas.Resolver;
using HydroMeasure.Application.Queries.Alertas.GetByCondominio;
using HydroMeasure.Application.Queries.Alertas.GetById;
using HydroMeasure.Application.Queries.Alertas.GetByUnidade;
using HydroMeasure.Application.Queries.Alertas.GetNaoResolvidos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlertaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAlertaCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpPut("{id}/resolver")]
        public async Task<IActionResult> Resolver(Guid id, [FromBody] Guid usuarioResolvidoId)
        {
            var command = new ResolverAlertaCommand
            {
                Id = id,
                UsuarioResolvidoId = usuarioResolvidoId
            };
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAlertaByIdQuery(id);
            var alerta = await _mediator.Send(query);

            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpGet("condominio/{condominioId}")]
        public async Task<IActionResult> GetByCondominio(Guid condominioId)
        {
            var query = new GetAlertasByCondominioQuery(condominioId);
            var alertas = await _mediator.Send(query);
            return Ok(alertas);
        }

        [HttpGet("unidade/{unidadeId}")]
        public async Task<IActionResult> GetByUnidade(Guid unidadeId)
        {
            var query = new GetAlertasByUnidadeQuery(unidadeId);
            var alertas = await _mediator.Send(query);
            return Ok(alertas);
        }

        [HttpGet("condominio/{condominioId}/nao-resolvidos")]
        public async Task<IActionResult> GetNaoResolvidos(Guid condominioId)
        {
            var query = new GetAlertasNaoResolvidosQuery(condominioId);
            var alertas = await _mediator.Send(query);
            return Ok(alertas);
        }
    }
}