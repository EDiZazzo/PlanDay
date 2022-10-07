using NUnit.Framework;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Infrastructure.Services;
using System.Threading.Tasks;

namespace Planday.Schedule.Infrastructure.Test;

public class GetExternalEmployeeTestsUnit
{
    private GetShiftByIdQuery _shiftByIdQuery;
    private GetEmployeeByExternalApiService _getEmployeeByExternalApiService;
    private GetEmployeeByExternalApiService _getEmployeeByExternalApiServiceUnAuthorized;

    [SetUp]
    public void SetUp()
    {
        _shiftByIdQuery = new GetShiftByIdQuery(new ConnectionStringProvider("Data Source=planday-schedule.db;"));
        var externalApiProvider = new ExternalApiStringProvider("http://planday-employee-api-techtest.westeurope.azurecontainer.io:5000/employee/", "8e0ac353-5ef1-4128-9687-fb9eb8647288");
        var externalApiProviderUnAuthorized = new ExternalApiStringProvider("http://planday-employee-api-techtest.westeurope.azurecontainer.io:5000/employee/", "00");
        _getEmployeeByExternalApiService = new GetEmployeeByExternalApiService(externalApiProvider);
        _getEmployeeByExternalApiServiceUnAuthorized = new GetEmployeeByExternalApiService(externalApiProviderUnAuthorized);
    }

    [Test]
    public async Task GetExternalEmployeeShouldBeNotNull()
    {
        var shiftId = new ShiftId(1);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        var employee = await _getEmployeeByExternalApiService.GetEmployee(new EmployeeId(shift.EmployeeId.Value));

        Assert.NotNull(employee);
    }

    [Test]
    public async Task GetExternalEmployeeShouldBeNullHavingUnauthorizeError()
    {
        var shiftId = new ShiftId(1);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        var employee = await _getEmployeeByExternalApiServiceUnAuthorized.GetEmployee(new EmployeeId(shift.EmployeeId.Value));

        Assert.Null(employee);
    }
}