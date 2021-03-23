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

namespace Application.UserManagements.Queries.UserGetAll
{
    public class UserGetAllQueries : IRequest<UsersVm>
    {
    }

    public class UserGetAllQueriesHandler : IRequestHandler<UserGetAllQueries, UsersVm>
    {
        private IUserService _userService;
        private readonly IMapper _mapper;


        public UserGetAllQueriesHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UsersVm> Handle(UserGetAllQueries request, CancellationToken cancellationToken)
        {
            return new UsersVm
            {
                UserDatas = await _userService.GetAll(cancellationToken)
            };
        }
    }
}
