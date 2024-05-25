using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Services;

public class TemperatureCalculationsService : ITemperatureCalculationsService
{

    public int ConvertToFahrenheit(int celsiusValueToConvert)
    {
        return (int)(celsiusValueToConvert * 9 / 5) + 32;
    }

    public int ConvertToCelsius(int fahrenheitValue)
    {
        return (int)((fahrenheitValue - 32) * 5) / 9;
    }
}