using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;
using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class UpdateShiftQuery : ICudShiftQuery<UpdateShiftCommand>
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public UpdateShiftQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
    
        public async Task QueryAsync(UpdateShiftCommand command)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDtos = await sqlConnection.ExecuteAsync(Sql, new
            {
                EmployeeId = command.EmployeeId.Value,
                ShiftId = command.ShiftId.Value,
            });
        }

        private const string Sql = @"UPDATE Shift SET EmployeeId = @EmployeeId WHERE Id = @ShiftId";
    }    
}

