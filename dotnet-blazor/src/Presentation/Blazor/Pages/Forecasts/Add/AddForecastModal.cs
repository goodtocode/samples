using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Pages.Forecasts.Add;

public partial class AddForecastModal
{
    [CascadingParameter] private BlazoredModalInstance Modal { get; set; } = default!;

    [Parameter] public AddForecastForm AddForecastForm { get; set; }

    [Inject] public IForecastService CoffeeService { get; set; }

    [Inject] public ITemperatureCalculationsService TemperatureCalculationsService { get; set; }

    public string ErrorMessage { get; set; }

    private async Task SubmitForm()
    {
        var zipcodeToAddVal = int.Parse(AddForecastForm.ZipcodesToString);

        AddForecastForm.Zipcodes.Clear();
        AddForecastForm.Zipcodes.Add(zipcodeToAddVal);

        var request = new AddForecastCommand
        {
            Date = AddForecastForm.Date,
            Key = AddForecastForm.Key,
            TemperatureF = AddForecastForm.TemperatureF,
            Zipcodes = AddForecastForm.Zipcodes
        };

        await CoffeeService.AddForecastCommandAsync(request);
        await Modal.CloseAsync(ModalResult.Ok(AddForecastForm));
    }


    private void AddZipcode(int zipcode)
    {
        if (!AddForecastForm.Zipcodes.Contains(zipcode))
            AddForecastForm.Zipcodes.Add(zipcode);
    }

    private void RemoveZipcode(int zipcode)
    {
        if (AddForecastForm.Zipcodes.Contains(zipcode))
            AddForecastForm.Zipcodes.Remove(zipcode);
    }

    private async Task OnFahrenheitValueChangedAsync(int celsiusValueToConvert)
    {
        AddForecastForm.TemperatureC = TemperatureCalculationsService.ConvertToCelsius(celsiusValueToConvert);
    }

    private async Task OnCelsiusValueChangedAsync(int fahrenheitValueToConvert)
    {
        var fahrenheitResult = TemperatureCalculationsService.ConvertToFahrenheit(fahrenheitValueToConvert);

        AddForecastForm.TemperatureF = fahrenheitResult;
    }
}