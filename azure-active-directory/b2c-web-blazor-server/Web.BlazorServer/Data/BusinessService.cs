using Goodtocode.Samples.Domain;
using System.Net;
using System.Text.Json;

namespace Goodtocode.Samples.BlazorServer.Data;

public class BusinessService
{
    private readonly IHttpClientFactory _clientFactory;

    public BusinessService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<IEnumerable<BusinessEntity>> GetBusinessesAsync(string name)
    {
        var httpClient = _clientFactory.CreateClient("SubjectsApiClient");
        
        var business = new List<BusinessEntity>() {  new BusinessEntity() { BusinessName = name } };

        return business;
    }
}