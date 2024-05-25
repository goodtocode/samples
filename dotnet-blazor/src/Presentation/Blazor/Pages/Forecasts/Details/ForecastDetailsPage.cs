using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;
using Coffee.Presentation.Blazor.Pages.Forecasts.Edit;

namespace Coffee.Presentation.Blazor.Pages.Forecasts.Details;

public partial class ForecastDetailsPage
{
    [Parameter] public Guid Key { get; set; }

    public ForecastVm ViewModel { get; set; }

    [Inject] private NavigationManager NavManager { get; set; }

    [Inject] private IForecastService WeatherForecastService { get; set; }

    [Inject] public IModalService Modal { get; set; }

    public string ErrorMessage { get; set; }

    protected async Task ShowEditModal()
    {
        try
        {
            var formModel = new EditForecastForm
            {
                Key = Key,
                Date = ViewModel.Date.LocalDateTime,
                TemperatureC = ViewModel.TemperatureC,
                TemperatureF = ViewModel.TemperatureF,
                Zipcodes = CreateZipcodes(ViewModel.Zipcodes),
                ZipcodesToString = string.Join(", ", ViewModel.Zipcodes)
            };

            var parameters = new ModalParameters().Add(nameof(EditForecastForm), formModel);

            var options = new ModalOptions
            {
                DisableBackgroundCancel = true
            };

            var confirmModal = Modal.Show<EditForecastModal>("Edit Forecast", parameters, options);

            var modalResult = await confirmModal.Result;

            switch (modalResult.Cancelled)
            {
                case false:
                {
                    if (!formModel.Deleted)
                    {
                        ViewModel.Date = formModel.Date;
                        ViewModel.TemperatureF = formModel.TemperatureF;
                        ViewModel.TemperatureC = formModel.TemperatureC;
                        ViewModel.Zipcodes = formModel.ZipcodesToString;
                    }

                    else
                    {
                        await NavigateToForecastsPage();
                    }
                }
                    break;
                case true:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private List<int> CreateZipcodes(string zipcodes)
    {
        var zipCodeSplit = zipcodes.Split(',').ToList();
        return zipCodeSplit.Select(int.Parse).ToList();
    }

    private async Task NavigateToForecastsPage()
    {
        NavManager.NavigateTo("/forecasts");
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await WeatherForecastService.GetForecastQueryAsync(Key);
        if (result.IsFailure)
            ErrorMessage = result.Error;
        else
            ViewModel = result.Value;
    }
}