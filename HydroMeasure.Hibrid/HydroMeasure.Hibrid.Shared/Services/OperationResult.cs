namespace HydroMeasure.Hibrid.Shared.Services
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public string? ErrorCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();

        public OperationResult()
        { }
    }
}
