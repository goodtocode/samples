using Coffee.Presentation.Blazor.Common;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Services;

public class ForecastService : IForecastService
{
    private readonly IApiClient _apiClient;
    private readonly string _apiVersion;
    private readonly IExceptionResultService _exceptionResultService;

    public ForecastService(IApiClient apiClient, IExceptionResultService exceptionResultService)
    {
        _exceptionResultService = exceptionResultService;
        _apiClient = apiClient;
        _apiVersion = "1";
    }

    public async Task<ForecastsVm> GetAllForecastsQueryAsync(string zipcodeFilter = "")
    {
        return await _apiClient.GetAllForecastsQueryAsync(zipcodeFilter, _apiVersion);
    }

    public async Task<Result<ForecastVm>> GetForecastQueryAsync(Guid key)
    {
        try
        {
            return Result.Success(await _apiClient.GetWeatherForecastQueryAsync(key, _apiVersion));
        }
        catch (Exception e)
        {
            return _exceptionResultService.Response<ForecastVm>(e);
        }
    }

    public async Task<Result<ForecastPaginatedDtoPaginatedList>> GetForecastsPaginatedQueryAsync(
        int pageNumber, int pageSize)
    {
        try
        {
            return Result.Success(await _apiClient.GetForecastsPaginatedQueryAsync(pageNumber, pageSize, _apiVersion));
        }
        catch (Exception e)
        {
            return _exceptionResultService.Response<ForecastPaginatedDtoPaginatedList>(e);
        }
    }

    public async Task AddForecastCommandAsync(AddForecastCommand command)
    {
        await _apiClient.AddForecastCommandAsync(_apiVersion, command);
    }

    public async Task PatchForecastCommandAsync(PatchForecastCommand command)
    {
        await _apiClient.PatchForecastCommandAsync(_apiVersion, command);
    }

    public async Task UpdateForecastCommandAsync(UpdateForecastCommand command)
    {
        await _apiClient.UpdateForecastCommandAsync(_apiVersion, command);
    }

    public async Task DeleteForecastCommandAsync(Guid key)
    {
        await _apiClient.RemoveForecastCommandAsync(key, _apiVersion);
    }

    public async Task Update(UpdateForecastCommand request)
    {
        await _apiClient.UpdateForecastCommandAsync(_apiVersion, new UpdateForecastCommand());
    }
}