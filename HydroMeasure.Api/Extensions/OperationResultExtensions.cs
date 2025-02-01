using HydroMeasure.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HydroMeasure.Api.Extensions
{
    public static class OperationResultExtensions
    {
        public static IActionResult ToActionResult<T>(this OperationResult<T> result)
        {
            if (result.Success)
            {
                // Se não houver dados, retorna NoContent (ou pode ser Ok, conforme o cenário)
                if (result.Data == null)
                    return new NoContentResult();

                return new OkObjectResult(result.Data);
            }
            else
            {
                // Retorna um BadRequest com detalhes dos erros
                return new BadRequestObjectResult(new
                {
                    result.ErrorCode,
                    result.Message,
                    result.Errors
                });
            }
        }
    }
}