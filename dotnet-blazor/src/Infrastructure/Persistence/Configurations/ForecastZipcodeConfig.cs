using Coffee.Core.Domain.Forecasts.Entities;

namespace Coffee.Infrastructure.Persistence.Configurations;

public class ForecastZipcodeConfig : IEntityTypeConfiguration<WeatherForecastZipcode>
{
    public void Configure(EntityTypeBuilder<WeatherForecastZipcode> builder)
    {
        builder.ToTable("ForecastZipCodes");
        builder.HasKey(x => x.Key);
        builder.Property(x => x.Key);
        builder
            .HasOne(x => x.WeatherForecast)
            .WithMany(x => x.ZipCodes);
    }
}