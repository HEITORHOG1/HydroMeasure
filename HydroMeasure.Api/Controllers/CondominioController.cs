using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.Condominios.Create;
using HydroMeasure.Application.Commands.Condominios.Delete;
using HydroMeasure.Application.Commands.Condominios.Update;
using HydroMeasure.Application.Queries.Condominios.Get;
using HydroMeasure.Application.Queries.Condominios.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondominioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CondominioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCondominioCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return result.ToActionResult(this, nameof(GetById), new { id = result.Data?.Id }, result.Data); // Usando a sobrecarga com CreatedAtAction
            return result.ToActionResult(); // Para erros, usa a sobrecarga padrão
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCondominioCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID inconsistente.");

            var result = await _mediator.Send(command);
            return result.ToActionResult(); // Usa a sobrecarga padrão (pode retornar 200 OK ou status de erro)
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCondominioCommand(id);
            var result = await _mediator.Send(command);
            return result.ToActionResult(); // Usa a sobrecarga para OperationResult<bool> (retorna 204 No Content em caso de sucesso)
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCondominioByIdQuery(id);
            var condominio = await _mediator.Send(query);
            if (condominio == null)
                return NotFound();
            return Ok(condominio);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCondominiosQuery();
            var condominios = await _mediator.Send(query);
            return Ok(condominios);
        }
    }
}