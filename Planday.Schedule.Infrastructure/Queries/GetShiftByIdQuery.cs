using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Dto;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetShiftByIdQuery : IGetEntityByIdQuery<ShiftId, Shift>
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public GetShiftByIdQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
    
        public async Task<Shift?> QueryFirstOrDefaultAsync(ShiftId shiftId)
        {
            var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDto = await sqlConnection.QueryFirstOrDefaultAsync<ShiftDto>(Sql, new { Id = shiftId.Value });
        
            return shiftDto is null ? null : new Shift(shiftDto.Id, shiftDto.EmployeeId, DateTime.Parse(shiftDto.Start), DateTime.Parse(shiftDto.End));
        }


        private const string Sql = @"SELECT Id, EmployeeId, Start, End FROM Shift WHERE Id = @Id;";
    }    
}

