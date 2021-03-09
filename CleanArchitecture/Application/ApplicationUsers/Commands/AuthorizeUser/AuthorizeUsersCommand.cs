using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUsers.Commands.AuthorizeUser
{
    public class AuthorizeUsersCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public string PolicyName { get; set; }
    }
    public class AuthorizeUsersCommandHandler : IRequestHandler<AuthorizeUsersCommand, bool>
    {
        private readonly IIdentityService _identityService;
        public AuthorizeUsersCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<bool> Handle(AuthorizeUsersCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AuthorizeAsync(request.UserId, request.PolicyName);
            return result;
        }
    }
}
