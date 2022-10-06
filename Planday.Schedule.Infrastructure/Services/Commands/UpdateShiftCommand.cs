namespace Planday.Schedule.Infrastructure.Services.Commands;

public record UpdateShiftCommand(ShiftId ShiftId, EmployeeId EmployeeId);
