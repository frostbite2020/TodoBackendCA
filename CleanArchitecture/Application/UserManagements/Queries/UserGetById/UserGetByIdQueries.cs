using Application.Common.Interfaces;
using Application.Common.Models.UserModels;
using Application.UserManagements.Queries.UserGetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Queries.UserGetById
{
    public class UserGetByIdQueries : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
    public class UserGetByIdQueriesHanlder : IRequestHandler<UserGetByIdQueries, UserModel>
    {
        private readonly IUserService _userService;

        public UserGetByIdQueriesHanlder(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserModel> Handle(UserGetByIdQueries request, CancellationToken cancellationToken)
        {
            return await _userService.GetById(request.Id);
        }

    }
}
