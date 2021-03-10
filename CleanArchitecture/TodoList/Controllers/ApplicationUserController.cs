using Application.ApplicationUsers.Commands.AuthorizeUser;
using Application.ApplicationUsers.Commands.DeleteUser;
using Application.ApplicationUsers.Commands.LoginUser;
using Application.ApplicationUsers.Commands.RegistrationUser;
using Application.ApplicationUsers.Queries.GetUser;
using Application.Common.Models;
using Application.Common.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    public class ApplicationUserController : ApiControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;

        }

        /*[HttpGet]
        [Route("UserName")]
        public async Task<ActionResult> GetUserName([FromQuery]GetUserQuery query)
        {
            return await Mediator.Send(query.UserId);
        }*/

        [HttpPost]
        [Route("Register")]
        public async Task<string> CreateRegister(CreateRegisterUsersCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<string> LoginMethod(LoginUsersCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost]
        [Route("Authorize")]
        public async Task<bool> AuthorizeMethod(AuthorizeUsersCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete("{userId}")]
        public async Task<Result> DeleteUser(DeleteUsersCommand command)
        {
            return await Mediator.Send(command);
        }


        /*[HttpPost]
        [Route("Register")]
        public async Task<Object> CreateRegister(ApplicationUserModel model)
        {
            model.Role = "User";
            var application = new ApplicationUser()
            {
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(application, model.Password);
            await _userManager.AddToRoleAsync(application, model.Role);
            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                //Get Role assigned to user
                var role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID", user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                return BadRequest(new { message = "Username or Password is incorrect." });
            }
        }*/
    }
}
