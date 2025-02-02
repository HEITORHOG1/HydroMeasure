using Grpc.Core;
using HydroMeasure.Api.Protos; // Namespace gerado pelo proto
using HydroMeasure.Application.Commands.Unidades.Create;
using HydroMeasure.Application.Commands.Unidades.Delete;
using HydroMeasure.Application.Commands.Unidades.Update;
using HydroMeasure.Application.DTOs;
using HydroMeasure.Application.Queries.Unidades.GetAll;
using HydroMeasure.Application.Queries.Unidades.GetById;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Api.Services.GrpcServices
{
    public class UnidadeService : UnidadeGrpcService.UnidadeGrpcServiceBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UnidadeService> _logger;

        public UnidadeService(IMediator mediator, ILogger<UnidadeService> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<CreateUnidadeResponse> CreateUnidade(CreateUnidadeRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando CreateUnidade gRPC");
            var command = new CreateUnidadeCommand
            {
                CondominioId = Guid.Parse(request.CondominioId),
                Identificacao = request.Identificacao,
                Tipo = request.Tipo.ToString(),
                MoradorResponsavel = request.MoradorResponsavel,
                MediaConsumo = (decimal)request.MediaConsumo // Convertendo double para decimal
            };

            var result = await _mediator.Send(command);
            var response = new CreateUnidadeResponse
            {
                Result = MapOperationResultToGrpc<UnidadeDto>(result)
            };
            _logger.LogInformation("Finalizando CreateUnidade gRPC");
            return response;
        }

        public override async Task<UpdateUnidadeResponse> UpdateUnidade(UpdateUnidadeRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando UpdateUnidade gRPC");
            var command = new UpdateUnidadeCommand
            {
                Id = Guid.Parse(request.Id),
                CondominioId = Guid.Parse(request.CondominioId),
                Identificacao = request.Identificacao,
                Tipo = request.Tipo,
                MoradorResponsavel = request.MoradorResponsavel,
                MediaConsumo = (decimal)request.MediaConsumo, // Convertendo double para decimal
                Ativo = request.Ativo,
                Status = request.Status
            };

            var result = await _mediator.Send(command);
            var response = new UpdateUnidadeResponse
            {
                Result = MapOperationResultToGrpc<UnidadeDto>(result)
            };
            _logger.LogInformation("Finalizando UpdateUnidade gRPC");
            return response;
        }

        public override async Task<GetUnidadeByIdResponse> GetUnidadeById(GetUnidadeByIdRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando GetUnidadeById gRPC");
            var query = new GetUnidadeByIdQuery(Guid.Parse(request.Id));
            var unidadeDto = await _mediator.Send(query);

            var response = new GetUnidadeByIdResponse
            {
                Unidade = unidadeDto != null ? MapUnidadeDtoToGrpc(unidadeDto) : null
            };

            _logger.LogInformation("Finalizando GetUnidadeById gRPC");
            return response;
        }

        public override async Task<GetAllUnidadesResponse> GetAllUnidades(GetAllUnidadesRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando GetAllUnidades gRPC");
            var query = new GetAllUnidadesQuery();
            var unidadesDto = await _mediator.Send(query);

            var response = new GetAllUnidadesResponse();
            foreach (var unidadeDto in unidadesDto)
            {
                response.Unidades.Add(MapUnidadeDtoToGrpc(unidadeDto));
            }
            _logger.LogInformation("Finalizando GetAllUnidades gRPC");
            return response;
        }

        public override async Task<DeleteUnidadeResponse> DeleteUnidade(DeleteUnidadeRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Iniciando DeleteUnidade gRPC");
            var command = new DeleteUnidadeCommand(Guid.Parse(request.Id));
            var result = await _mediator.Send(command);

            var response = new DeleteUnidadeResponse
            {
                Result = new OperationResultGrpcBoolUnidade
                {
                    Success = result.Success,
                    Message = result.Message,
                    Data = result.Data, // bool do OperationResult
                    ErrorCode = result.ErrorCode ?? string.Empty,
                    Errors = { result.Errors }
                }
            };

            _logger.LogInformation("Finalizando DeleteUnidade gRPC");
            return response;
        }

        private OperationResultGrpcUnidadeDto MapOperationResultToGrpc<TDto>(OperationResult<TDto> result) where TDto : UnidadeDto
        {
            var grpcResult = new OperationResultGrpcUnidadeDto
            {
                Success = result.Success,
                Message = result.Message,
                ErrorCode = result.ErrorCode ?? string.Empty,
                Errors = { result.Errors }
            };

            if (result.Data != null)
            {
                grpcResult.Data = MapUnidadeDtoToGrpc(result.Data);
            }
            return grpcResult;
        }

        private UnidadeGrpcDto MapUnidadeDtoToGrpc(UnidadeDto unidadeDto)
        {
            return new UnidadeGrpcDto
            {
                Id = unidadeDto.Id.ToString(),
                CondominioId = unidadeDto.CondominioId.ToString(),
                Identificacao = unidadeDto.Identificacao,
                Tipo = (TipoUnidade)Enum.Parse(typeof(TipoUnidade), unidadeDto.Tipo),
                MoradorResponsavel = unidadeDto.MoradorResponsavel ?? string.Empty,
                MediaConsumo = (double)unidadeDto.MediaConsumo,
                Ativo = unidadeDto.Ativo,
                Status = (StatusUnidade)Enum.Parse(typeof(StatusUnidade), unidadeDto.Status)
            };
        }
    }
}