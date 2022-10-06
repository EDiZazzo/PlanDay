using Microsoft.Data.Sqlite;
using Moq;
using NUnit.Framework;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Infrastructure.Services;
using Planday.Schedule.Infrastructure.Services.Commands;
using Planday.Schedule.Queries;
using System;
using System.Threading.Tasks;

namespace Planday.Schedule.Infrastructure.Test;

public class NewOpenShiftTestsUnit
{
    private InsertNewShiftService _newShiftService;
    private GetShiftByIdQuery _shiftByIdQuery;

    [SetUp]
    public void SetUp()
    {
        var connectionString = new ConnectionStringProvider("Data Source=planday-schedule.db;");
        _newShiftService = new InsertNewShiftService(new InsertNewShiftQuery(connectionString));
        _shiftByIdQuery = new GetShiftByIdQuery(connectionString);
    }

    [Test]
    public async Task InsertNewOpenShiftShouldBeOK()
    {
        var command = new InsertShiftCommand(DateTime.Today.AddDays(1).AddHours(8), DateTime.Today.AddDays(1).AddHours(16));

        var success = await _newShiftService.InsertNewShift(command);

        var shift = await _shiftByIdQuery.QueryFirstOrDefaultAsync(new ShiftId(3));

        Assert.IsTrue(success);
        Assert.AreEqual(3, shift.Id);
        Assert.Null(shift.EmployeeId);
        Assert.AreEqual(DateTime.Today.AddDays(1).AddHours(8), shift.Start);
        Assert.AreEqual(DateTime.Today.AddDays(1).AddHours(16), shift.End);
    }

    [Test]
    public async Task InsertNewOpenShiftShouldNotBeOkHavingStartGreaterThanEnd()
    {
        var command = new InsertShiftCommand(DateTime.Today.AddDays(1).AddHours(16), DateTime.Today.AddDays(1).AddHours(8));

        var success = await _newShiftService.InsertNewShift(command);

        Assert.IsFalse(success);
    }

    [Test]
    public async Task InsertNewOpenShiftShouldNotBeOkHavingStartDayDifferentThanEndDay()
    {
        var command = new InsertShiftCommand(DateTime.Today.AddDays(1).AddHours(8), DateTime.Today.AddDays(2).AddHours(16));

        var success = await _newShiftService.InsertNewShift(command);

        Assert.IsFalse(success);
    }

    [Test]
    public async Task InsertNewOpenShiftShouldNotBeOkHavingStartYearDifferentThanEndYear()
    {
        var command = new InsertShiftCommand(DateTime.Today.AddYears(1).AddHours(8), DateTime.Today.AddYears(2).AddHours(16));

        var success = await _newShiftService.InsertNewShift(command);

        Assert.IsFalse(success);
    }
}