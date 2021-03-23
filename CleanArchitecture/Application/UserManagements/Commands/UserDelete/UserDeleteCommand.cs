using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UserDelete
{
    public class UserDeleteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, int>
    {
        private readonly IUserService _userService;

        public UserDeleteCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<int> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.Delete(request.Id, cancellationToken);

            return user;
        }
    }
}
