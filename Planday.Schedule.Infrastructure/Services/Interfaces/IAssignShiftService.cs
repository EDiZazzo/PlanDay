using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Infrastructure.Services;

public interface IAssignShiftService
{
    public Task<bool> UpdateShift(UpdateShiftCommand command);
}
