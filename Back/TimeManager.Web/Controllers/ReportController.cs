using Microsoft.AspNetCore.Mvc;
using TimeManager.Data.Entities;
using TimeManager.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using System;
using TimeManager.Web.Exceptions;

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
        public async Task<IActionResult> GetReportsByUserId(int userId)
        {
            try
            {
                return Ok(await repository.GetReportsByUserIdAsync(userId));
            }
            catch(Exception ex)
            {
                return BadRequest(PostgreCustomExceptionHandler.Handle(ex));
            }
        }
    }
}