using Planday.Schedule.Dto;
using Planday.Schedule.Infrastructure.Services.Commands;

namespace Planday.Schedule.Queries
{
    public interface ICudShiftQuery<TCommand>
    {
        Task QueryAsync(TCommand command);
    }    
}

