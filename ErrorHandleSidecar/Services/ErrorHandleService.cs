using ErrorHandleSidecar.Protos;
using Grpc.Core;

namespace ErrorHandleSidecar.Services
{
    public class ErrorHandleService : ErrorHandler.ErrorHandlerBase
    {
        private readonly ILogger<ErrorHandleService> _logger;
        public ErrorHandleService(ILogger<ErrorHandleService> logger)
        {
            _logger = logger;
        }

        public override Task<ErrorResponse> GetErrorResponse(ErrorRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Handling Error Service");

            return Task.FromResult(new ErrorResponse
            {
                Message = $"This is test message for code {request.ErrorCode}"
            });
        }
    }
}
