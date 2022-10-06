using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Dto;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetEmployeeByIdQuery : IGetEntityByIdQuery<EmployeeId, Employee>
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public GetEmployeeByIdQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
    
        public async Task<Employee?> QueryFirstOrDefaultAsync(EmployeeId employeeId)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var employeeDto = await sqlConnection.QueryFirstOrDefaultAsync<EmployeeDto>(Sql, new { Id = employeeId.Value});
        
            return employeeDto is null ? null : new Employee(employeeDto.Id, employeeDto.Name);
        }

        private const string Sql = @"SELECT Id, Name FROM Employee WHERE Id = @Id;";
    }    
}

