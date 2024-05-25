using AutoMapper.QueryableExtensions;
using Coffee.Core.Application.Common.Interfaces;
using Coffee.Core.Application.Common.Mappings;
using Coffee.Core.Application.Common.Models;

namespace Coffee.Core.Application.ForecastLists.Queries.GetPaginated;

public class GetForecastsPaginatedQuery : IRequest<PaginatedList<ForecastPaginatedDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class
    GetCoffeePaginatedQueryHandler : IRequestHandler<GetForecastsPaginatedQuery,
        PaginatedList<ForecastPaginatedDto>>
{
    private readonly ICoffeeContext _context;
    private readonly IMapper _mapper;

    public GetCoffeePaginatedQueryHandler(ICoffeeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ForecastPaginatedDto>> Handle(GetForecastsPaginatedQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedCoffee = await _context.ForecastViews
            .AsNoTracking()
            .OrderByDescending(x => x.ForecastDate)
            .ProjectTo<ForecastPaginatedDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        foreach (var item in paginatedCoffee.Items) item.TemperatureC = (item.TemperatureF - 32) * 5 / 9;

        return paginatedCoffee;
    }
}