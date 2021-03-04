using Application.Common.Models;
using Infrastructure.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Commands.CreateRegister
{
    public class CreateRegisterCommand : IRequest<string>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    public class CreateRegisterCommandHandler : IRequestHandler<CreateRegisterCommand, string>
    {
        private readonly IdentityService _identityService;

        public CreateRegisterCommandHandler(IdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<string> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.CreateUserAsync(request.UserName, request.Password);

            return result;
        }
    }
}
