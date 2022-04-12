namespace ErrorHandleSidecar.Models
{
    public class ErrorSchema
    {
        public string? ErrorCode { get; set; }
        public string? Name { get; set; }
        public string? ErrorMessage { get; set; }
        public bool CanRetry { get; set; }
        public int NoOfRetry { get; set; }
        public string? RequestId { get; set; }
    }
}
