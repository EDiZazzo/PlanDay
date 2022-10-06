using Planday.Schedule.Request;
using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Api;

public static class MappingExtensions
{
    public static InsertShiftCommand ToCommand(this InsertShiftRequest insertShiftRequest)
        => new InsertShiftCommand(insertShiftRequest.Start, insertShiftRequest.End);

    public static UpdateShiftCommand ToCommand(this AssignShiftRequest assignShiftRequest)
        => new UpdateShiftCommand(new ShiftId(assignShiftRequest.ShiftId), new EmployeeId(assignShiftRequest.EmployeeId));
}