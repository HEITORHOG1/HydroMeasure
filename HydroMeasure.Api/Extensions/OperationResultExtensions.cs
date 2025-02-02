using HydroMeasure.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Extensions
{
    public static class OperationResultExtensions
    {
        // Overload para OperationResult<T> onde T é o DTO a ser retornado (ex: GetById, Create, Update)
        public static IActionResult ToActionResult<T>(this OperationResult<T> result, ControllerBase controller = null, string actionName = null, object routeValues = null, object value = null)
        {
            if (result.Success)
            {
                if (actionName != null && controller != null)
                {
                    // Retorna 201 Created com CreatedAtAction se actionName e controller forem fornecidos (para POST)
                    return controller.CreatedAtAction(actionName, routeValues, value);
                }
                else if (result.Data != null)
                {
                    // Retorna 200 OK com os dados se houver Data (para GET, PUT, PATCH)
                    return new OkObjectResult(result.Data);
                }
                else
                {
                    // Retorna 204 No Content se Success mas sem Data (para operações como Update sem retorno, ou Delete)
                    return new NoContentResult();
                }
            }
            else
            {
                // Lógica de tratamento de erro baseada no ErrorCode
                switch (result.ErrorCode)
                {
                    case "NotFound":
                        return new NotFoundObjectResult(new ProblemDetails
                        {
                            Status = 404,
                            Title = "Recurso Não Encontrado",
                            Detail = result.Message,
                            Extensions = { { "errors", result.Errors } }
                        });

                    case "InvalidInput":
                    case "ValidationFailed": // Adicione "ValidationFailed" caso use validações
                        return new BadRequestObjectResult(new ProblemDetails
                        {
                            Status = 400,
                            Title = "Requisição Inválida",
                            Detail = result.Message,
                            Extensions = { { "errors", result.Errors } }
                        });

                    case "Unauthorized":
                        return new UnauthorizedResult(); // 401 Unauthorized
                    case "Forbidden":
                        return new ForbidResult();       // 403 Forbidden
                    // Adicione mais casos de ErrorCode conforme necessário (ex: "Conflict", "ServerError", etc.)
                    default:
                        // Caso genérico para outros erros não mapeados explicitamente, retorna 400 BadRequest por padrão
                        return new BadRequestObjectResult(new ProblemDetails
                        {
                            Status = 400, // Ou 500 para erros de servidor desconhecidos, dependendo da sua política
                            Title = "Erro na Requisição",
                            Detail = "Ocorreu um erro ao processar a requisição.", // Mensagem genérica ou result.Message
                            Extensions = { { "errors", result.Errors } } // Pode estar vazio ou conter detalhes
                        });
                }
            }
        }

        // Overload para OperationResult<bool> (ex: Delete, operações que retornam apenas sucesso ou falha booleana)
        public static IActionResult ToActionResult(this OperationResult<bool> result)
        {
            if (result.Success && result.Data) // Assumindo que Data = true significa sucesso em operações booleanas
            {
                return new NoContentResult(); // 204 No Content para Delete bem-sucedido
            }
            else if (result.Success && !result.Data)
            {
                // Caso raro: Operação booleana "bem-sucedida" mas Data é false (pode indicar "já estava deletado", etc., dependendo da lógica)
                return new OkResult(); // Ou outro status code apropriado, talvez 200 OK com mensagem no corpo se precisar informar algo
            }
            else
            {
                // Lógica de tratamento de erro para OperationResult<bool> (similar ao outro overload)
                switch (result.ErrorCode)
                {
                    case "NotFound":
                        return new NotFoundObjectResult(new ProblemDetails
                        {
                            Status = 404,
                            Title = "Recurso Não Encontrado",
                            Detail = result.Message,
                            Extensions = { { "errors", result.Errors } }
                        });

                    case "InvalidInput":
                    case "ValidationFailed":
                        return new BadRequestObjectResult(new ProblemDetails
                        {
                            Status = 400,
                            Title = "Requisição Inválida",
                            Detail = result.Message,
                            Extensions = { { "errors", result.Errors } }
                        });

                    case "Unauthorized":
                        return new UnauthorizedResult();

                    case "Forbidden":
                        return new ForbidResult();

                    default:
                        return new BadRequestObjectResult(new ProblemDetails
                        {
                            Status = 400, // Ou 500
                            Title = "Erro na Requisição",
                            Detail = "Ocorreu um erro ao processar a requisição.",
                            Extensions = { { "errors", result.Errors } }
                        });
                }
            }
        }
    }
}