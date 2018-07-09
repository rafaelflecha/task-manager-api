using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using TaskManagerAPI.Repositories;
using TaskManager.API.Models;
using TaskManager.API.ViewModels;
using TaskManager.API.Helpers;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")] 
    public class AccountsController : Controller
    {
        private readonly TaskManagerDbContext _taskManagerContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, TaskManagerDbContext taskManagerContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _taskManagerContext = taskManagerContext;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _taskManagerContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id });
            await _taskManagerContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}