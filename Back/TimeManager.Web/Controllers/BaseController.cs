using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeManager.Data.Repositories.Interfaces;
using TimeManager.Web.Exceptions;

namespace TimeManager.Web.Controllers
{
    public class BaseController<TEntity> : ControllerBase
        where TEntity : class
    {
        private readonly IBaseRepository<TEntity> repository;

        public BaseController(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await repository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await repository.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(TEntity entity)
        {
            try
            {
                var result = await repository.CreateAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(PostgreCustomException.Handle(ex));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(TEntity entity)
        {
            try
            {            
                var result = await repository.UpdateAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(PostgreCustomException.Handle(ex));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(TEntity entity)
        {
            try
            {
                var result = await repository.DeleteAsync(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message);
            }
        }
    }
}