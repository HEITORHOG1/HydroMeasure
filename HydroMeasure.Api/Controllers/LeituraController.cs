using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Leituras.Create;
using HydroMeasure.Application.Commands.Leituras.Delete;
using HydroMeasure.Application.Commands.Leituras.Update;
using HydroMeasure.Application.Queries.Leituras.GetByHidrometro;
using HydroMeasure.Application.Queries.Leituras.GetById;
using HydroMeasure.Application.Queries.Leituras.GetByUnidade;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeituraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeituraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLeituraCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetLeituraByIdQuery(id);
            var leitura = await _mediator.Send(query);
            if (leitura == null)
                return NotFound();
            return Ok(leitura);
        }

        [HttpGet("hidrometros/{hidrometroId}")]
        public async Task<IActionResult> GetByHidrometroId(Guid hidrometroId)
        {
            var query = new GetLeiturasByHidrometroQuery(hidrometroId);
            var leituras = await _mediator.Send(query);
            return Ok(leituras);
        }

        [HttpGet("unidades/{unidadeId}")]
        public async Task<IActionResult> GetByUnidadeId(Guid unidadeId)
        {
            var query = new GetLeiturasByUnidadeQuery(unidadeId);
            var leituras = await _mediator.Send(query);
            return Ok(leituras);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeituraCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID inconsistente.");

            command.Id = id; // Garante que o ID da rota seja usado
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteLeituraCommand(id);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}