namespace Coffee.Core.Application.ForecastCalculations.Queries.GetCelsiusToFahrenheit;

public class
    GetCelsiusToFahrenheitCalculationConversionQueryValidator : AbstractValidator<
        GetCelsiusToFahrenheitCalculationConversionQuery>
{
    public GetCelsiusToFahrenheitCalculationConversionQueryValidator()
    {
        RuleFor(x => x.CelsiusValue).NotNull();
    }
}