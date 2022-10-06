using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planday.Schedule.Queries
{
    public interface IGetEntityByIdQuery<TId, Tout>
    {
        Task<Tout> QueryFirstOrDefaultAsync(TId id);
    }    
}

