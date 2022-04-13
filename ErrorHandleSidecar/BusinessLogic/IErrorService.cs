using ErrorHandleSidecar.Protos;

namespace ErrorHandleSidecar.BusinessLogic;

public interface IErrorService
{
    Task<ErrorResponse> GetErrorResponse(ErrorRequest request);
}