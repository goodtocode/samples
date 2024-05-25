using Coffee.Core.Domain.Forecasts.Entities;
using Coffee.Core.Domain.Forecasts.Models;

namespace Coffee.Core.Application.Common.Interfaces;

public interface ICoffeeContext
{
    DbSet<ForecastsView> ForecastViews { get; }
    DbSet<Forecast> Forecasts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}