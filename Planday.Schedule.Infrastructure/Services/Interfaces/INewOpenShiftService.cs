using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Infrastructure.Services;

public interface INewOpenShiftService
{
    public Task<bool> InsertNewShift(InsertShiftCommand command);
}
