using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Pages.Forecasts.Edit;

public partial class EditForecastModal
{
    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = default!;

    [Parameter] public EditForecastForm EditForecastForm { get; set; }

    [Inject] public IForecastService CoffeeService { get; set; }

    [Inject] public ITemperatureCalculationsExternalService WeatherCalculationsService { get; set; }

    public string ErrorMessage { get; set; }


    private async Task SubmitForm()
    {
        var zipcodeToAddVal = EditForecastForm.ZipcodesToString.Split(",");

        EditForecastForm.Zipcodes.Clear();

        foreach (var zipcode in zipcodeToAddVal.Select(int.Parse)) EditForecastForm.Zipcodes.Add(zipcode);

        var request = new UpdateForecastCommand
        {
            Date = EditForecastForm.Date,
            Key = EditForecastForm.Key,
            TemperatureF = EditForecastForm.TemperatureF,
            Zipcodes = EditForecastForm.Zipcodes
        };

        await CoffeeService.UpdateForecastCommandAsync(request);
        await Modal.CloseAsync(ModalResult.Ok(EditForecastForm));
    }

    public async Task DeleteForecast()
    {
        await CoffeeService.DeleteForecastCommandAsync(EditForecastForm.Key);
        EditForecastForm.Deleted = true;
        await Modal.CloseAsync(ModalResult.Ok(EditForecastForm));
    }

    private async Task OnFahrenheitValueChangedAsync(int value)
    {
        var celsiusResult = await WeatherCalculationsService.ConvertToCelsiusAsync(value);

        if (celsiusResult.IsFailure)
            ErrorMessage = celsiusResult.Error;

        else if (celsiusResult.Value != EditForecastForm.TemperatureC)
            EditForecastForm.TemperatureC = celsiusResult.Value;
    }

    private async Task OnCelsiusValueChangedAsync(int value)
    {
        var fahrenheitResult = await WeatherCalculationsService.ConvertToFahrenheitAsync(value);

        if (fahrenheitResult.IsFailure)
            ErrorMessage = fahrenheitResult.Error;

        else if (fahrenheitResult.Value != EditForecastForm.TemperatureC)
            EditForecastForm.TemperatureF = fahrenheitResult.Value;
    }
    
    private void Cancel()
    {
        Modal.CancelAsync();
    }
}