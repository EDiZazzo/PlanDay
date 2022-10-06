using Microsoft.Data.Sqlite;
using Moq;
using NUnit.Framework;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Queries;
using System.Threading.Tasks;

namespace Planday.Schedule.Infrastructure.Test;

public class GetShiftTestUnit
{
    private GetShiftByIdQuery _shiftByIdQuery;


    [SetUp]
    public void SetUp()
    {
        var connectionString = new ConnectionStringProvider("Data Source=planday-schedule.db;");
        _shiftByIdQuery = new GetShiftByIdQuery(connectionString);
    }

    [Test]
    public async Task GetShiftByIdQueryShouldBeOK()
    {
        var shiftId = new ShiftId(1);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        Assert.AreEqual(shiftId.Value, shift.Id);
    }

    [Test]
    public async Task GetShiftByIdQueryShouldBeNull()
    {
        var shiftId = new ShiftId(3);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        Assert.Null(shift);
    }
}