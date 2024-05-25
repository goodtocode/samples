namespace Coffee.Presentation.Blazor.Common.Interfaces;

public interface IForecastService
{
    Task<ForecastsVm> GetAllForecastsQueryAsync(string zipcodeFilter = "");
    Task<Result<ForecastVm>> GetForecastQueryAsync(Guid key);

    Task<Result<ForecastPaginatedDtoPaginatedList>> GetForecastsPaginatedQueryAsync(int pageNumber,
        int pageSize);

    Task AddForecastCommandAsync(AddForecastCommand request);
    Task UpdateForecastCommandAsync(UpdateForecastCommand request);
    Task PatchForecastCommandAsync(PatchForecastCommand request);
    Task DeleteForecastCommandAsync(Guid key);
}