using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace UDCTestTask.Core.Models.Auxiliary;

public class ApiError
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public string? ErrorDetails { get; set; }

    public ApiError()
    {
    }

    public ApiError(int errorCode, string errorMessage, string? errorDetails = null)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        ErrorDetails = errorDetails;
    }

    public override string ToString()
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        JsonSerializerSettings serializeOptions = new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        };
        
        return JsonConvert.SerializeObject(this, serializeOptions);
    }
}
