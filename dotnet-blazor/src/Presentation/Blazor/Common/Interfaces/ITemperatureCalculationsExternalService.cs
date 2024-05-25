namespace Coffee.Presentation.Blazor.Common.Interfaces;

public interface ITemperatureCalculationsExternalService
{
    Task<Result<int>> ConvertToFahrenheitAsync(int fahrenheitValueToConvert);
    Task<Result<int>> ConvertToCelsiusAsync(int fahrenheitValue);
}