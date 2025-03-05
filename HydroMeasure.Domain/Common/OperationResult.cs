namespace HydroMeasure.Domain.Common
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}