using Newtonsoft.Json;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace Planday.Schedule.Infrastructure.Services;

public class GetEmployeeByExternalApiService : IGetEmployeeByExternalApiService
{
    private readonly IExternalApiStringProvider _provider;

    public GetEmployeeByExternalApiService(IExternalApiStringProvider provider)
    {
        _provider = provider;
    }
    public async Task<ExternalEmployee> GetEmployee(EmployeeId employeeId)
    {
        var client = new HttpClient();

        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _provider.GetKey());
        client.DefaultRequestHeaders.Add("Authorization", _provider.GetKey());
        var baseUri = new Uri(_provider.GetUri());

        var response = await client.GetAsync(
            new Uri(baseUri, employeeId.Value.ToString())
            );

        return response.StatusCode == HttpStatusCode.OK ? JsonConvert.DeserializeObject<ExternalEmployee>(await response.Content.ReadAsStringAsync()!) : null;
    }
}
