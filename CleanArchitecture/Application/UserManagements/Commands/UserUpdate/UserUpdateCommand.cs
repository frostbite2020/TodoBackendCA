using Application.Common.Interfaces;
using Application.Common.Models.UserModels;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UserUpdate
{
    public class UserUpdateCommand : IRequest<UpdateModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, UpdateModel>
    {
        private readonly IUserService _userService;

        public UserUpdateCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateModel> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            var user = new UserProperty();
            user.Id = request.Id;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Username = request.Username;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            return await _userService.Update(user, cancellationToken, request.Password);
        }
    }
}
