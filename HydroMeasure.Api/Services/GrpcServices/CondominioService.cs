using Grpc.Core;
using HydroMeasure.Api.Protos;
using HydroMeasure.Application.Commands.Condominios.Create;
using HydroMeasure.Application.Commands.Condominios.Delete;
using HydroMeasure.Application.Commands.Condominios.Update;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Application.Queries.Condominios.Get;
using HydroMeasure.Application.Queries.Condominios.GetAll;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Api.Services.GrpcServices
{
    public class CondominioService : CondominioGrpcService.CondominioGrpcServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CondominioService> _logger;

        public CondominioService(IMediator mediator, ILogger<CondominioService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CreateCondominioResponse> CreateCondominio(CreateCondominioRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando CreateCondominio gRPC");
            var command = new CreateCondominioCommand
            {
                Nome = request.Nome,
                Endereco = request.Endereco,
                CNPJ = request.Cnpj,
                Sindico = request.Sindico,
                TelefoneSindico = request.TelefoneSindico,
                EmailSindico = request.EmailSindico
            };

            var result = await _mediator.Send(command);
            var response = new CreateCondominioResponse
            {
                Result = MapOperationResultToGrpc<CondominioDto>(result) // Chamada corrigida
            };
            _logger.LogInformation("Finalizando CreateCondominio gRPC");
            return response;
        }

        public override async Task<UpdateCondominioResponse> UpdateCondominio(UpdateCondominioRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando UpdateCondominio gRPC");
            var command = new UpdateCondominioCommand
            {
                Id = Guid.Parse(request.Id),
                Nome = request.Nome,
                Endereco = request.Endereco,
                CNPJ = request.Cnpj,
                Sindico = request.Sindico,
                TelefoneSindico = request.TelefoneSindico,
                EmailSindico = request.EmailSindico,
                Ativo = request.Ativo
            };

            var result = await _mediator.Send(command);
            var response = new UpdateCondominioResponse
            {
                Result = MapOperationResultToGrpc<CondominioDto>(result) // Chamada corrigida
            };
            _logger.LogInformation("Finalizando UpdateCondominio gRPC");
            return response;
        }

        public override async Task<GetCondominioByIdResponse> GetCondominioById(GetCondominioByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando GetCondominioById gRPC");
            var query = new GetCondominioByIdQuery(Guid.Parse(request.Id));
            var condominioDto = await _mediator.Send(query);

            var response = new GetCondominioByIdResponse();
            if (condominioDto != null)
            {
                response.Condominio = MapCondominioDtoToGrpc(condominioDto);
            }
            // Se condominioDto for null, response.Condominio permanece null (comportamento padrão do proto)
            _logger.LogInformation("Finalizando GetCondominioById gRPC");
            return response;
        }

        public override async Task<GetAllCondominiosResponse> GetAllCondominios(GetAllCondominiosRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando GetAllCondominios gRPC");
            var query = new GetAllCondominiosQuery();
            var condominiosDto = await _mediator.Send(query);

            var response = new GetAllCondominiosResponse();
            foreach (var condominioDto in condominiosDto)
            {
                response.Condominios.Add(MapCondominioDtoToGrpc(condominioDto));
            }
            _logger.LogInformation("Finalizando GetAllCondominios gRPC");
            return response;
        }

        public override async Task<DeleteCondominioResponse> DeleteCondominio(DeleteCondominioRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando DeleteCondominio gRPC");
            var command = new DeleteCondominioCommand(Guid.Parse(request.Id));
            var result = await _mediator.Send(command);

            var response = new DeleteCondominioResponse
            {
                Result = new OperationResultGrpcBool
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = result.Data,
                    ErrorCode = result.ErrorCode ?? string.Empty, // Garante que não seja null
                    Errors = { result.Errors } // Adiciona a lista de erros
                }
            };
            _logger.LogInformation("Finalizando DeleteCondominio gRPC");
            return response;
        }

        // Método corrigido - removemos TGrcpDto e a restrição incorreta
        private OperationResultGrpcCondominioDto MapOperationResultToGrpc<TDto>(OperationResult<TDto> result) where TDto : CondominioDto
        {
            var grpcResult = new OperationResultGrpcCondominioDto
            {
                Success = result.Success,
                Message = result.Message,
                ErrorCode = result.ErrorCode ?? string.Empty, // Garante que não seja null
                Errors = { result.Errors } // Adiciona a lista de erros
            };

            if (result.Data != null)
            {
                grpcResult.Data = MapCondominioDtoToGrpc(result.Data);
            }
            return grpcResult;
        }

        private CondominioGrpcDto MapCondominioDtoToGrpc(CondominioDto condominioDto)
        {
            return new CondominioGrpcDto
            {
                Id = condominioDto.Id.ToString(),
                Nome = condominioDto.Nome,
                Endereco = condominioDto.Endereco,
                Cnpj = condominioDto.CNPJ ?? string.Empty, // Garante que não seja null
                Sindico = condominioDto.Sindico ?? string.Empty, // Garante que não seja null
                TelefoneSindico = condominioDto.TelefoneSindico ?? string.Empty, // Garante que não seja null
                EmailSindico = condominioDto.EmailSindico ?? string.Empty, // Garante que não seja null
                Ativo = condominioDto.Ativo
            };
        }
    }
}