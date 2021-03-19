using Microsoft.AspNetCore.Mvc;
using TimeManager.Data.Entities;
using TimeManager.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : BaseController<Report>
    {
        private readonly IReportRepository repository;

        public ReportController(IReportRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        [HttpGet("GetReportsByUserId/{userId}")]
        public async  Task<IEnumerable<Report>>  GetReportsByUserId(int userId)
        {
            return await repository.GetReportsByUserIdAsync(userId);
        }
    }
}