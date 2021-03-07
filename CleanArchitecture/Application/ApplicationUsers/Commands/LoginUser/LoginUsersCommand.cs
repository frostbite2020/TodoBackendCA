using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUsers.Commands.LoginUser
{
    public class LoginUsersCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginUsersCommandHandler : IRequestHandler<LoginUsersCommand, string>
    {
        private readonly IIdentityService _identityService;
        
        public LoginUsersCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(LoginUsersCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.LoginAsync(request.UserName, request.Password);
        }
    }
}
