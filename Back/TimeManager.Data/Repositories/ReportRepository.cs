using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeManager.Data.Entities;
using TimeManager.Data.Repositories.Interfaces;

namespace TimeManager.Data.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        private readonly ApplicationDbContext context;
        public ReportRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Report>> GetReportsByUserIdAsync(int userId)
        {
            return await context.Set<Report>().Where(x => 
                x.UserId == userId).Include(x => x.User).ToListAsync();
        }
    }
}