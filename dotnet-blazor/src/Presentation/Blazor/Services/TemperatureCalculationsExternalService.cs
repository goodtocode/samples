using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Services;

public class TemperatureCalculationsExternalService : ITemperatureCalculationsExternalService
{
    private readonly IApiClient _apiClient;
    private readonly string _apiVersion;
    private readonly IExceptionResultService _exceptionResultService;

    public TemperatureCalculationsExternalService(IApiClient apiClient, IExceptionResultService genericExceptionResponse)
    {
        _apiClient = apiClient;
        _apiVersion = "1";
        _exceptionResultService = genericExceptionResponse;
    }

    public async Task<Result<int>> ConvertToFahrenheitAsync(int fahrenheitValueToConvert)
    {
        try
        {
            var result = await _apiClient.GetFahrenheitToCelsiusCalculationConversionQueryAsync(fahrenheitValueToConvert, _apiVersion);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return _exceptionResultService.Response<int>(e);
        }
    }

    public async Task<Result<int>> ConvertToCelsiusAsync(int fahrenheitValue)
    {
        try
        {
            var result = await _apiClient.GetFahrenheitToCelsiusCalculationConversionQueryAsync((int) fahrenheitValue, _apiVersion);
            return Result.Success(result);
        }
        catch (Exception e)
        {
            return _exceptionResultService.Response<int>(e);
        }
    }
}

