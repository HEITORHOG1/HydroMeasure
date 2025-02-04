using HydroMeasure.Api.Extensions;
using HydroMeasure.Application.Commands.ConfiguracoesCondominio.Create;
using HydroMeasure.Application.Commands.ConfiguracoesCondominio.Delete;
using HydroMeasure.Application.Commands.ConfiguracoesCondominio.Update;
using HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetAll;
using HydroMeasure.Application.Queries.ConfiguracoesCondominio.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracaoCondominioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfiguracaoCondominioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConfiguracaoCondominioCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
                return CreatedAtAction(nameof(GetById), new { id = result.Data?.Id }, result.Data);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateConfiguracaoCondominioCommand command)
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
            var query = new GetConfiguracaoCondominioByIdQuery(id);
            var configuracaoCondominio = await _mediator.Send(query);
            if (configuracaoCondominio == null)
                return NotFound();
            return Ok(configuracaoCondominio);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllConfiguracoesCondominioQuery();
            var configuracoesCondominio = await _mediator.Send(query);
            return Ok(configuracoesCondominio);
        }

        // Por enquanto, vamos omitir o Delete para ConfiguracaoCondominio, pois pode não ser uma operação comum.
        // Se precisar, podemos adicionar o DeleteCommand, DeleteCommandHandler e o endpoint Delete no Controller depois.

        [HttpDelete("{id}")] // Endpoint Delete
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteConfiguracaoCondominioCommand(id); // Pass 'id' to the constructor
            var result = await _mediator.Send(command);
            return result.ToActionResult();
        }
    }
}