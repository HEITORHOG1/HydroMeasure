using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Hidrometros.Create;
using HydroMeasure.Application.Commands.Hidrometros.Delete;
using HydroMeasure.Application.Commands.Hidrometros.Update;
using HydroMeasure.Application.Queries.Hidrometros.GetAll;
using HydroMeasure.Application.Queries.Hidrometros.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HidrometroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HidrometroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateHidrometroCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHidrometroCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID inconsistente.");

            command.Id = id; // Garante que o ID da rota seja usado
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetHidrometroByIdQuery(id);
            var hidrometro = await _mediator.Send(query);
            if (hidrometro == null)
                return NotFound();
            return Ok(hidrometro);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllHidrometrosQuery();
            var hidrometros = await _mediator.Send(query);
            return Ok(hidrometros);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteHidrometroCommand(id);
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}