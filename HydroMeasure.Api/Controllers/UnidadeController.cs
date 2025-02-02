using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Unidades.Create;
using HydroMeasure.Application.Commands.Unidades.Delete;
using HydroMeasure.Application.Commands.Unidades.Update;
using HydroMeasure.Application.Queries.Unidades.GetAll;
using HydroMeasure.Application.Queries.Unidades.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnidadeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUnidadeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUnidadeCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID inconsistente.");

            command.Id = id; // Garante que o ID da rota seja usado no command
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetUnidadeByIdQuery(id);
            var unidade = await _mediator.Send(query);
            if (unidade == null)
                return NotFound();
            return Ok(unidade);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllUnidadesQuery();
            var unidades = await _mediator.Send(query);
            return Ok(unidades);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUnidadeCommand(id);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}