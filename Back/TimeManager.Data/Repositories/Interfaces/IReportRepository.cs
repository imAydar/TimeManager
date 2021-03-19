using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeManager.Data.Entities;

namespace TimeManager.Data.Repositories.Interfaces
{
    public interface IReportRepository:IBaseRepository<Report>
    {
        Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId);
    }
}