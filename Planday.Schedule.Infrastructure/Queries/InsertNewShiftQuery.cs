using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;
using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class InsertNewShiftQuery : ICudShiftQuery<InsertShiftCommand>
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public InsertNewShiftQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
    
        public async Task QueryAsync(InsertShiftCommand command)
        {
            var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            await sqlConnection.ExecuteAsync(Sql, new { Start = command.Start.ToString("yyyy-MM-dd HH:mm:ss.fff"), End = command.End.ToString("yyyy-MM-dd HH:mm:ss.fff")});

        }

        //Useful for Test
        public async Task<long> GetLastShiftId()
        {
            var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            return await sqlConnection.QueryFirstOrDefaultAsync<long>(SqlLastId);
        }

        private const string Sql = @"INSERT INTO Shift (Start, End) VALUES (@Start, @End);";
        private const string SqlLastId = "SELECT Id FROM Shift WHERE EmployeeId is null ORDER BY Id Desc";
    }    
}

