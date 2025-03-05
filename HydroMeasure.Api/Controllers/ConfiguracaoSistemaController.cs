using HydroMeasure.Application.Commands.ConfiguracaoSistema;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Application.Queries.ConfiguracaoSistema;
using HydroMeasure.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfiguracaoSistemaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfiguracaoSistemaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ConfiguracaoSistemaDto>> Get()
        {
            var query = new GetConfiguracaoSistemaQuery();
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<OperationResult<ConfiguracaoSistemaDto>>> Create([FromBody] CreateConfiguracaoSistemaCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResult<ConfiguracaoSistemaDto>>> Update(Guid id, [FromBody] UpdateConfiguracaoSistemaCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID na rota não corresponde ao ID no corpo da requisição.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("reset")]
        public async Task<ActionResult<OperationResult<bool>>> Reset()
        {
            var command = new ResetConfiguracaoSistemaCommand();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}