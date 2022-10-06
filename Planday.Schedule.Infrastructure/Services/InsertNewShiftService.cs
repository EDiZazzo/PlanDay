using Planday.Schedule.Infrastructure.Services.Commands;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Services;

public class InsertNewShiftService : INewOpenShiftService
{
    private ICudShiftQuery<InsertShiftCommand> _insertNewShiftQuery;

    public InsertNewShiftService(ICudShiftQuery<InsertShiftCommand> insertNewShiftQuery)
    {
        _insertNewShiftQuery = insertNewShiftQuery;
    }
    public async Task<bool> InsertNewShift(InsertShiftCommand command)
    {
        if (command is not null &&
            command.Start.Year == command.End.Year &&
            command.Start.DayOfYear == command.End.DayOfYear &&
            command.Start.Hour < command.End.Hour)
        {
            try
            {
                await _insertNewShiftQuery.QueryAsync(command);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
