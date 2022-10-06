using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Infrastructure.Services.Commands;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Services;

public class AssignShiftService : IAssignShiftService
{
    private ICudShiftQuery<UpdateShiftCommand> _updateShiftQuery;
    private IGetEntityByIdQuery<ShiftId, Shift> _shiftByShiftIdQuery;
    private IGetEntityByIdQuery<EmployeeId, Employee> _employeeByEmployeeIdQuery;
    private IGetEntityByIdQuery<EmployeeId, IList<Shift>> _shiftByEmployeeIdQuery;

    public AssignShiftService(
        ICudShiftQuery<UpdateShiftCommand> updateShiftQuery,
        IGetEntityByIdQuery<ShiftId, Shift> shiftByShiftIdQuery,
        IGetEntityByIdQuery<EmployeeId, Employee> employeeByEmployeeIdQuery,
        IGetEntityByIdQuery<EmployeeId, IList<Shift>> shiftByEmployeeIdQuery)
    {
        _updateShiftQuery = updateShiftQuery;
        _shiftByShiftIdQuery = shiftByShiftIdQuery;
        _employeeByEmployeeIdQuery = employeeByEmployeeIdQuery;
        _shiftByEmployeeIdQuery = shiftByEmployeeIdQuery;
    }
    public async Task<bool> UpdateShift(UpdateShiftCommand command)
    {
        var shiftToAssign = await _shiftByShiftIdQuery.QueryFirstOrDefaultAsync(command.ShiftId);
        if (shiftToAssign == null || shiftToAssign.EmployeeId is not null)
            return false;

        var employee = await _employeeByEmployeeIdQuery.QueryFirstOrDefaultAsync(command.EmployeeId);
        if (employee == null)
            return false;

        var shiftListToControl = await _shiftByEmployeeIdQuery.QueryFirstOrDefaultAsync(command.EmployeeId);

        foreach(var shiftToControl in shiftListToControl)
        {
            if(!shiftToControl.Compatible(shiftToAssign))
                return false;
        }

        try
        {
            _updateShiftQuery.QueryAsync(command);
        }
        catch (Exception ex)
        {
            return false;
        }       

        return true;
    }
}
