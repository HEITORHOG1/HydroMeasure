﻿using HydroMeasure.Application.DTOs;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Commands.Condominios.Create
{
    public class CreateCondominioCommand : IRequest<OperationResult<CondominioDto>>
    {
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string? CNPJ { get; set; }
        public string? Sindico { get; set; }
        public string? TelefoneSindico { get; set; }
        public string? EmailSindico { get; set; }
    }
}