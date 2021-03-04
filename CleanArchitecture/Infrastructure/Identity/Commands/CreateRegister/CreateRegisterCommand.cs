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
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateRegisterCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var application = new ApplicationUser();
            application.UserName = request.UserName;
            
            await _userManager.CreateAsync(application, request.Password);

            return application.Id;
        }
    }
}
