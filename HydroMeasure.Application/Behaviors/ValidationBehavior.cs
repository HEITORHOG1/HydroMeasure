using FluentValidation;
using HydroMeasure.Shared;
using MediatR;

namespace HydroMeasure.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, OperationResult<TResponse>>
     where TRequest : IRequest<OperationResult<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<OperationResult<TResponse>> Handle(
            TRequest request,
            RequestHandlerDelegate<OperationResult<TResponse>> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Any())
                {
                    var operationResult = new OperationResult<TResponse>
                    {
                        Success = false,
                        Message = "Erro de validação.",
                        Errors = failures.Select(f => f.ErrorMessage).ToList(),
                        ErrorCode = failures.FirstOrDefault()?.ErrorCode
                    };
                    return operationResult;
                }
            }

            return await next();
        }
    }
}