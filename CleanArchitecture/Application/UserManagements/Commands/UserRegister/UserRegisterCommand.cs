using Application.Common.Interfaces;
using Application.Common.Models.UserModels;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UserRegister
{
    public class UserRegisterCommand : IRequest<UserProperties>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }

    public class UserRegisterCommandHandler : IRequestHandler <UserRegisterCommand, UserProperties>
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserRegisterCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserProperties> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {

            var model = new UserProperties();
            model.FirstName = request.FirstName;
            model.LastName = request.LastName;
            model.Username = request.UserName;
            model.Email = request.Email;
            model.PhoneNumber = request.PhoneNumber;
            model.Role = Role.User;

            var user = await _userService.Create(model, request.Password, cancellationToken);

            return user;
        }
    }
}
