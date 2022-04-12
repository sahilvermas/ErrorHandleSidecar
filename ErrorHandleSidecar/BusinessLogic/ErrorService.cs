using System.Globalization;
using AutoMapper;
using ErrorHandleSidecar.Models;
using ErrorHandleSidecar.Protos;
using Newtonsoft.Json;

namespace ErrorHandleSidecar.BusinessLogic
{
    public class ErrorService : IErrorService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ErrorService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ErrorResponse> GetErrorResponse(ErrorRequest request)
        {
            var errorSchemaFilePath = _configuration.GetValue<string>("ErrorSchemaPath");

            var errorSchema = await GetErrorSchema(errorSchemaFilePath);
            if (errorSchema == null) return new ErrorResponse { ErrorMessage = "Error: Unable to read the error schema." };

            var errorDescription = errorSchema.FirstOrDefault(x =>
                                       x.ErrorCode != null && x.ErrorCode.Equals(request.ErrorCode))
                                   ?? errorSchema.First(x => x.ErrorCode is "1");

            //var dd = _mapper.Map<ErrorResponse>(errorDescription);

            var errorResponse = new ErrorResponse()
            {
                ErrorCode = errorDescription.ErrorCode,
                ErrorMessage = errorDescription.ErrorMessage,
                CanRetry = errorDescription.CanRetry,
                Name = errorDescription.Name,
                NoOfRetries = errorDescription.NoOfRetries,
                RequestId = string.IsNullOrEmpty(errorDescription.RequestId) ? "0" : errorDescription.RequestId,
            };

            return errorResponse;
        }


        private static async Task<List<ErrorSchema>?> GetErrorSchema(string errorSchemaFilePath)
        {
            var errorSchema = new List<ErrorSchema>();
            using var reader = new StreamReader(errorSchemaFilePath);
            if (reader.Peek() != 0)
            {
                errorSchema = JsonConvert.DeserializeObject<List<ErrorSchema>>(await reader.ReadToEndAsync());
            }
            return errorSchema ?? null;
        }
    }
}
