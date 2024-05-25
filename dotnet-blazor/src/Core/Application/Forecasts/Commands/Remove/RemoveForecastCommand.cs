using Coffee.Core.Application.Common.Exceptions;
using Coffee.Core.Application.Common.Interfaces;

namespace Coffee.Core.Application.Forecasts.Commands.Remove;

public class RemoveForecastCommand : IRequest
{
    public Guid Key { get; set; }
}

public class RemoveForecastCommandHandler : IRequestHandler<RemoveForecastCommand>
{
    private readonly ICoffeeContext _context;

    public RemoveForecastCommandHandler(ICoffeeContext context)
    {
        _context = context;
    }

    public async Task Handle(RemoveForecastCommand request, CancellationToken cancellationToken)
    {
        var weatherForecast = _context.Forecasts.Find(request.Key);

        if (weatherForecast == null) throw new NotFoundException();
        _context.Forecasts.Remove(weatherForecast);
        await _context.SaveChangesAsync(cancellationToken);
    }
}