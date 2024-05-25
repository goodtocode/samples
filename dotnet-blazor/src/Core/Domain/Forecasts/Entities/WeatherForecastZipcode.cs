namespace Coffee.Core.Domain.Forecasts.Entities;

public class WeatherForecastZipcode : Common.Entity
{
    protected WeatherForecastZipcode() { }

    public WeatherForecastZipcode(int zipCode, Forecast weatherForecast)
    {
        ZipCode = zipCode;
        WeatherForecast = weatherForecast;
    }

    public virtual int ZipCode { get; private set; }

    public virtual Forecast WeatherForecast { get; private set; }
}