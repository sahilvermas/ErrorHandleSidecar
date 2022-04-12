using ErrorHandleSidecar.Models;

namespace ErrorHandleSidecar.BusinessLogic
{
    public class ErrorService : IErrorService
    {
        public Task<Protos.ErrorResponse> GetErrorResponse(Protos.ErrorRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
