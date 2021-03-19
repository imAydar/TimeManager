using Microsoft.AspNetCore.Mvc;
using TimeManager.Data.Entities;
using TimeManager.Data.Repositories.Interfaces;

namespace TimeManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(IUserRepository repository) : base(repository)
        {
        }
    }
}