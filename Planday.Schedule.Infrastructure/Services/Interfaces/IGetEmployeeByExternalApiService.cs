using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Infrastructure.Services;

public interface IGetEmployeeByExternalApiService
{
    public Task<ExternalEmployee> GetEmployee(EmployeeId employeeId);
}
