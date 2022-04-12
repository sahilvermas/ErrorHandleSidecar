namespace ErrorHandleSidecar.BusinessLogic
{
    public interface IErrorService
    {
        Task<Protos.ErrorResponse> GetErrorResponse(Protos.ErrorRequest request);
    }
}
