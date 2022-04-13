using ErrorHandleSidecar.Models;
using ErrorHandleSidecar.Protos;
using ErrorHandleSidecar.Utilities;

namespace ErrorHandleSidecar.BusinessLogic;

public class ErrorService : IErrorService
{
    private readonly IConfiguration _configuration;

    public ErrorService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<ErrorResponse> GetErrorResponse(ErrorRequest request)
    {
        var errorSchemaFilePath = _configuration.GetValue<string>("ErrorSchemaPath");

        var errorSchema = await GetErrorSchema(errorSchemaFilePath);
        if (errorSchema == null) return GetDefaultResponse();

        var err = errorSchema.FirstOrDefault(x =>
            x.ErrorCode != null && x.ErrorCode.Equals(request.ErrorCode));

        if (err == null) return GetDefaultResponse();

        return new ErrorResponse
        {
            ErrorCode = err.ErrorCode,
            Name = err.Name,
            ErrorMessage = err.ErrorMessage,
            CanRetry = err.CanRetry,
            NoOfRetries = err.NoOfRetries,
            RequestId = err.RequestId,
            PublishToDlq = err.PublishToDlq,
            Category = err.Category
        };
    }

    private static ErrorResponse GetDefaultResponse()
    {
        return new ErrorResponse
        {
            ErrorCode = "1",
            Name = "Default Error",
            ErrorMessage = "The error is not defined",
            CanRetry = false,
            NoOfRetries = 0,
            RequestId = "0",
            PublishToDlq = false,
            Category = "NA"
        };
    }


    private static async Task<List<ErrorSchema>?> GetErrorSchema(string errorSchemaFilePath)
    {
        var errorSchema = new List<ErrorSchema>();
        using var reader = new StreamReader(errorSchemaFilePath);
        if (reader.Peek() != 0) errorSchema = FormatUtil.Deserialize<List<ErrorSchema>>(await reader.ReadToEndAsync());
        return errorSchema;
    }
}