using Infrastructure.Identity.Commands.CreateRegister;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public ApplicationUserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<Object> CreateRegister(ApplicationUserModel model)
        {
            var application = new ApplicationUser
            {
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(application, model.Password);

            return Ok(result);
/*            return await Mediator.Send(command);
*/      }
    }
}
