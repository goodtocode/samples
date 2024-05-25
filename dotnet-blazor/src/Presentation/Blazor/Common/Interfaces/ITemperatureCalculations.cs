namespace Coffee.Presentation.Blazor.Common.Interfaces;

public interface ITemperatureCalculationsService
{
    int ConvertToFahrenheit(int celsiusValueToConvert);
    int ConvertToCelsius(int fahrenheitValue);
}

