using System.Runtime.Serialization;

namespace ErrorHandleSidecar.Models;

[DataContract]
internal class ErrorSchema
{
    [DataMember(Name = "errorCode")] public string? ErrorCode { get; set; }

    [DataMember(Name = "name")] public string? Name { get; set; }

    [DataMember(Name = "errorMessage")] public string? ErrorMessage { get; set; }

    [DataMember(Name = "canRetry")] public bool CanRetry { get; set; }

    [DataMember(Name = "noOfRetries")] public int NoOfRetries { get; set; }

    [DataMember(Name = "requestId")] public string? RequestId { get; set; }

    [DataMember(Name = "publishToDlq")] public bool PublishToDlq { get; set; }

    [DataMember(Name = "category")] public string? Category { get; set; }
}