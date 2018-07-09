using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Repositories;
using TaskManager.API.ViewModels;

namespace TaskManager.API.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    //[ApiController]
    public class AppTaskController : ControllerBase
    {
        private readonly IAppTaskRepository _appTaskRepository;
        private readonly ClaimsPrincipal _caller;
        public AppTaskController(IAppTaskRepository appTaskRepository, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appTaskRepository = appTaskRepository;
        }
        // // GET api/apptask
        [HttpGet]
        public ActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _caller.Claims.Single(c => c.Type == "id");

            return new OkObjectResult(_appTaskRepository.GetAll().Where(p => p.UserId == Guid.Parse(userId.Value)));
        }

        // POST api/apptask
        [HttpPost]
        public ActionResult Post([FromBody]AppTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _caller.Claims.Single(c => c.Type == "id");

            var newAppTask = new AppTask
            {
                Title = model.Title,
                UserId = Guid.Parse(userId.Value),
                Description = model.Description,
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            _appTaskRepository.Create(newAppTask);
            return new OkObjectResult(newAppTask);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]AppTaskViewModel model)
        {
            var userId = _caller.Claims.Single(c => c.Type == "id");

            var newAppTask = new AppTask
            {
                Title = model.Title,
                UserId = Guid.Parse(userId.Value),
                Description = model.Description,
                IsCompleted = model.IsCompleted
            };

            _appTaskRepository.Update(id, newAppTask);
        }

        // DELETE api/apptask/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _appTaskRepository.Delete(id);
        }
    }
}
