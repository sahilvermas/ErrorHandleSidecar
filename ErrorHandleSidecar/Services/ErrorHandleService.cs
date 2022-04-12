using ErrorHandleSidecar.BusinessLogic;
using ErrorHandleSidecar.Protos;
using Grpc.Core;

namespace ErrorHandleSidecar.Services
{
    public class ErrorHandleService : ErrorHandler.ErrorHandlerBase
    {
        private readonly ILogger<ErrorHandleService> _logger;
        private readonly IErrorService _errorService;


        public ErrorHandleService(ILogger<ErrorHandleService> logger, IErrorService errorService)
        {
            _logger = logger;
            _errorService = errorService;
        }

        public override async Task<ErrorResponse> GetErrorResponse(ErrorRequest request, ServerCallContext context)
        {
            try
            {
                return await _errorService.GetErrorResponse(request);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occurred: {e.StackTrace}");
                throw;
            }
        }
    }
}
