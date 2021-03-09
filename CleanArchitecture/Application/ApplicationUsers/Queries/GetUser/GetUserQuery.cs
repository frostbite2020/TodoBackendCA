using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ApplicationUsers.Queries.GetUser
{
    public class GetUserQuery : IRequest<string>
    {
        public string UserId { get; set; }
    }
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, string>
    {
        private readonly IIdentityService _identityService;
        public GetUserQueryHandler (IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<string> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _identityService.GetUserNameAsync(request.UserId);
            return result;
        }
    }
}
