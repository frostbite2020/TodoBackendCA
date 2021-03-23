using Application.Common.Models;
using Application.Common.Models.UserModels;
using Application.UserManagements.Commands.UserDelete;
using Application.UserManagements.Commands.UserLogin;
using Application.UserManagements.Commands.UserRegister;
using Application.UserManagements.Commands.UserUpdate;
using Application.UserManagements.Queries.UserGetAll;
using Application.UserManagements.Queries.UserGetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [Route("auth")]
    public class UserManagementController : ApiControllerBase
    {
        [HttpPost]
        [Route("register")]

        public async Task<UserProperties> Create(UserRegisterCommand command)
        {
            return await Mediator.Send(command);
        } 

        [HttpPost]
        [Route("login")]
        public async Task<UserLoginSuccessDto> Login(UserLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<UsersVm>> Get()
        {
            return await Mediator.Send(new UserGetAllQueries());
        }

        [Authorize]
        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var queries = new UserGetByIdQueries();
            queries.Id = id;
            if (id != queries.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(queries);
        }

        [Authorize]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<UpdateModel>> Update(int id, UserUpdateCommand command)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = new UserUpdateCommand();
            user.Id = id;
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Username = command.Username;
            user.Email = command.Email;
            user.PhoneNumber = command.PhoneNumber;
            user.Password = command.Password;

            if (id != command.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(user);
        }

        [Authorize]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            return await Mediator.Send(new UserDeleteCommand { Id = id });
        }
    }
}
