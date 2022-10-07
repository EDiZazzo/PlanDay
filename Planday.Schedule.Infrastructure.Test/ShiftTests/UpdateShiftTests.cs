using NUnit.Framework;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Infrastructure.Services;
using Planday.Schedule.Infrastructure.Services.Commands;
using System;
using System.Threading.Tasks;

namespace Planday.Schedule.Infrastructure.Test;

public class UpdateShiftTestsUnit
{
    private AssignShiftService _AssignShiftService;
    private GetShiftByIdQuery _shiftByIdQuery;
    private InsertNewShiftQuery _newShiftquery;

    [SetUp]
    public void SetUp()
    {
        var connectionString = new ConnectionStringProvider("Data Source=planday-schedule.db;");
        _AssignShiftService = new AssignShiftService(
            new UpdateShiftQuery(connectionString),
            new GetShiftByIdQuery(connectionString),
            new GetEmployeeByIdQuery(connectionString),
            new GetShiftByEmployeeIdQuery(connectionString));
        _shiftByIdQuery = new GetShiftByIdQuery(connectionString);
        _newShiftquery = new InsertNewShiftQuery(connectionString);

        _newShiftquery.QueryAsync(new InsertShiftCommand(DateTime.Today.AddDays(1).AddHours(8), DateTime.Today.AddDays(1).AddHours(16)));
    }

    [Test]
    public async Task UpdateShiftShouldBeOK()
    {
        var shiftId = new ShiftId(await _newShiftquery.GetLastShiftId());

        var command = new UpdateShiftCommand(shiftId, new EmployeeId(1));

        var success = await _AssignShiftService.UpdateShift(command);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        Assert.IsTrue(success);
        Assert.AreEqual(shiftId.Value, shift.Id);
        Assert.IsNotNull(shift.EmployeeId);
    }

    [Test]
    public async Task UpdateShiftShouldNotBeOkHavingShiftAlreadyAssigned()
    {
        var shiftId = new ShiftId(2);

        var command = new UpdateShiftCommand(shiftId, new EmployeeId(1));

        var success = await _AssignShiftService.UpdateShift(command);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        Assert.IsFalse(success);
        Assert.AreNotEqual(shift.EmployeeId, command.EmployeeId);        
    }

    [Test]
    public async Task UpdateShiftShouldNotBeOkHavingOverlappingShiftAssigned()
    {
        var shiftId = new ShiftId(await _newShiftquery.GetLastShiftId());

        var command = new UpdateShiftCommand(shiftId, new EmployeeId(1));

        await _AssignShiftService.UpdateShift(command);

        _newShiftquery.QueryAsync(new InsertShiftCommand(DateTime.Today.AddDays(1).AddHours(12), DateTime.Today.AddDays(1).AddHours(20)));

        shiftId = new ShiftId(await _newShiftquery.GetLastShiftId());

        var success = await _AssignShiftService.UpdateShift(command);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(shiftId);

        Assert.IsFalse(success);
        Assert.IsNull(shift.EmployeeId);
    }
}