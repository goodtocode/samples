using Coffee.Core.Application.Common.Exceptions;
using Coffee.Core.Application.Common.Interfaces;

namespace Coffee.Core.Application.Forecasts.Commands.Update;

public class UpdateForecastCommand : IRequest
{
    public Guid Key { get; set; }
    public DateTime Date { get; set; }
    public int? TemperatureF { get; set; }
    public List<int> Zipcodes { get; set; }
}

public class UpdateWeatherForecastCommandHandler : IRequestHandler<UpdateForecastCommand>
{
    private readonly ICoffeeContext _context;

    public UpdateWeatherForecastCommandHandler(ICoffeeContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);
        if (weatherForecast == null) throw new NotFoundException();

        weatherForecast.UpdateDate(request.Date);
        weatherForecast.UpdateTemperatureF((int) request.TemperatureF);
        weatherForecast.UpdateZipcodes(request.Zipcodes);
        _context.Forecasts.Update(weatherForecast);
        await _context.SaveChangesAsync(CancellationToken.None);
    }
}