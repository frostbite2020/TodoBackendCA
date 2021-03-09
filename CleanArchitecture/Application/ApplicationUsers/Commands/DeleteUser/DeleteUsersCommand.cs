using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUsers.Commands.DeleteUser
{
    public class DeleteUsersCommand : IRequest<Result>
    {
        public string UserId { get; set; }
    }
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public DeleteUsersCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteUserAsync(request.UserId);
            return result;
        }
    }
}
