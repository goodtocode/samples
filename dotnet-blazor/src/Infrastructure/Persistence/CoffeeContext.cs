using System.Reflection;
using Coffee.Core.Application.Common.Interfaces;
using Coffee.Core.Domain.Forecasts.Entities;
using Coffee.Core.Domain.Forecasts.Models;

namespace Coffee.Infrastructure.Persistence;

public partial class CoffeeContext : DbContext, ICoffeeContext
{
    protected CoffeeContext() { }

    public CoffeeContext(DbContextOptions<CoffeeContext> options) : base(options) { }

    public DbSet<ForecastsView> ForecastViews => Set<ForecastsView>();

    public DbSet<Forecast> Forecasts => Set<Forecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            x => x.Namespace == $"{GetType().Namespace}.Configurations");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}