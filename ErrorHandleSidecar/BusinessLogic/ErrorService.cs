using System.Globalization;
using ErrorHandleSidecar.Models;
using ErrorHandleSidecar.Protos;
using Newtonsoft.Json;

namespace ErrorHandleSidecar.BusinessLogic
{
    public class ErrorService : IErrorService
    {
        public async Task<ErrorResponse> GetErrorResponse(ErrorRequest request)
        {
            var errorSchema = await GetErrorSchema();
            if (errorSchema == null) return new ErrorResponse { ErrorMessage = "Error: Unable to read the error schema." };

            var errorDescription = errorSchema.FirstOrDefault(x =>
                                       x.ErrorCode != null && x.ErrorCode.Equals(request.ErrorCode))
                                   ?? errorSchema.First(x => x.ErrorCode is "1");

            var errorResponse = new ErrorResponse()
            {
                ErrorCode = errorDescription.ErrorCode,
                ErrorMessage = errorDescription.ErrorMessage,
                CanRetry = errorDescription.CanRetry,
                Name = errorDescription.Name,
                NoOfRetries = errorDescription.NoOfRetry,
                RequestId = string.IsNullOrEmpty(errorDescription.RequestId) ? "0" : errorDescription.RequestId,
                Time = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            };

            return errorResponse;
        }


        private static async Task<List<ErrorSchema>?> GetErrorSchema()
        {
            var errorSchema = new List<ErrorSchema>();

            using var reader = new StreamReader("Schemas/error-schema.json");
            if (reader.Peek() != 0)
            {
                errorSchema = JsonConvert.DeserializeObject<List<ErrorSchema>>(await reader.ReadToEndAsync());
            }
            return errorSchema ?? null;
        }
    }
}
