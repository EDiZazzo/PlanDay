using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Dto;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Infrastructure.Services.Commands;
using Planday.Schedule.Queries;
using System.Collections.Generic;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetShiftByEmployeeIdQuery : IGetEntityByIdQuery<EmployeeId, IList<Shift>>
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public GetShiftByEmployeeIdQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<IList<Shift>> QueryFirstOrDefaultAsync(EmployeeId id)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDtos = await sqlConnection.QueryAsync<ShiftDto>(Sql, new
            {
                EmployeeId = id.Value
            });

            var shifts = shiftDtos.Select(x =>
                new Shift(x.Id, x.EmployeeId, DateTime.Parse(x.Start), DateTime.Parse(x.End)));

            return shifts.ToList();
        }

        private const string Sql = @"SELECT Id, EmployeeId, Start, End FROM Shift WHERE EmployeeId = @EmployeeId ;";
    }    
}

