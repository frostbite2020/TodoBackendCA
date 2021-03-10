using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.IdentityModels;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUsers.Commands.RegistrationUser
{
    public class CreateRegisterUsersCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class CreateRegisterUsersCommandHandler : IRequestHandler<CreateRegisterUsersCommand, string>
    {
        private readonly IIdentityService _identityService;

        public CreateRegisterUsersCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(CreateRegisterUsersCommand request, CancellationToken cancellationToken)
        {
            request.Role = "Admin";

            var result = await _identityService.CreateUserAsync(request.UserName, request.Password, request.Role);

            return result.UserId;
        }
    }
}
