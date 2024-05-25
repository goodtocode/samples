using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coffee.Presentation.Blazor.Common;

public class ApiException : Exception
{
    public ApiException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>>
            headers, Exception innerException) : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" +
                                                      (response == null
                                                          ? "(null)"
                                                          : response.Substring(0,
                                                              response.Length >= 512 ? 512 : response.Length)),
        innerException)
    {
        StatusCode = statusCode;
        Response = response;
        Headers = headers;
    }

    public ApiException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public int StatusCode { get; }

    public string Response { get; }

    public IDictionary<string, string[]> Errors { get; set; }

    public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; }

    public override string ToString()
    {
        return $"HTTP Response: \n\n{Response}\n\n{base.ToString()}";
    }
}

public class ApiException<TResult> : ApiException
{
    public ApiException(string message, int statusCode, string response,
        IReadOnlyDictionary<string, IEnumerable<string>>
            headers, TResult result, Exception innerException) : base(message, statusCode, response, headers,
        innerException)
    {
        Result = result;

        if (statusCode == 404) throw new Exception("Not Found");

        if (statusCode != 400) return;
        if (result is not ProblemDetails brr) return;
        var validationFailures = new List<ValidationFailure>();
        var additionalProperties = brr.AdditionalProperties;
        var errorsObject = additionalProperties["errors"];
        var errors = JObject.FromObject(errorsObject).ToObject<Dictionary<string, string[]>>();
        foreach (var specificError in errors)
        {
            var errorTypeErrors = JsonConvert.SerializeObject(specificError.Value);
            var errorsList = JsonConvert.DeserializeObject<List<string>>(errorTypeErrors);

            if (errorsList != null)
                validationFailures.AddRange(errorsList.Select(errorMessage =>
                    new ValidationFailure(specificError.Key, errorMessage)));

            Errors = validationFailures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }

    public TResult Result { get; }
}