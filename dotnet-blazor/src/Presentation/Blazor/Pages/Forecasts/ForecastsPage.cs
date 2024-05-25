using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;
using Coffee.Presentation.Blazor.Pages.Forecasts.Add;

namespace Coffee.Presentation.Blazor.Pages.Forecasts;

public partial class ForecastsPage
{
    private ForecastPaginatedDtoPaginatedList ViewModel;

    [CascadingParameter] public IModalService Modal { get; set; }

    [Parameter] public int? Page { get; set; }

    [Parameter] public int? PageSize { get; set; }

    [Inject] private NavigationManager NavManager { get; set; }

    [Inject] private IForecastService WeatherForecastService { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    private async Task ShowAddWeatherForecastModal()
    {
        try
        {
            var formModel = new AddForecastForm();
            var parameters = new ModalParameters()
                .Add(nameof(AddForecastForm), formModel);
            var options = new ModalOptions
            {
                DisableBackgroundCancel = true
            };

            var confirmModal = Modal.Show<AddForecastModal>("Add Forecast", parameters, options);
            var modalResult = await confirmModal.Result;

            switch (modalResult.Cancelled)
            {
                case false:
                    NavManager.NavigateTo($"/forecasts/{formModel.Key}");
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

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await RefreshViewModel();
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }
    }

    private async Task SetAndRefreshViewModel(int page)
    {
        Page = page;
        await RefreshViewModel();
    }

    private async Task RefreshViewModel()
    {
        var defaultPageSize = 10;
        var defaultPage = 1;

        Page = Page ?? defaultPage;
        PageSize = PageSize ?? defaultPageSize;

        var result = await WeatherForecastService.GetForecastsPaginatedQueryAsync(Page ?? defaultPage,
            PageSize ?? defaultPageSize);

        if (result.IsFailure)
            ErrorMessage = result.Error;
        else

            ViewModel = result.Value;

        StateHasChanged();
    }

    private async Task NavigateToDetailsPage(Guid key)
    {
        NavManager.NavigateTo($"/forecasts/{key}");
    }
}