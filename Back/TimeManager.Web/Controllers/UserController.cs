using Microsoft.AspNetCore.Mvc;
using TimeManager.Data.Entities;
using TimeManager.Data.Repositories.Interfaces;

namespace TimeManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<User>
    {
        private readonly IUserRepository repository;

        public UserController(IUserRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        /*[HttpGet]
        public override async Task<IActionResult> Get()
        {
            return Ok(await repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public override async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.GetByIdAsync(id));
        }*/
    }
}