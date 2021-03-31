using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Queries.GetAllTodoDailys
{
    public class GetAllTodoDailyQuery : IRequest<IList<TodoDailyDto>>
    {
        public int UserPropertyId { get; set; }
    }

    public class GetAllTodoDailyQueryHandler : IRequestHandler<GetAllTodoDailyQuery, IList<TodoDailyDto>>
    {
        private ITodoDaily _todoDaily;
        public GetAllTodoDailyQueryHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }
        public async Task<IList<TodoDailyDto>> Handle(GetAllTodoDailyQuery request, CancellationToken cancellationToken)
        {
            return await _todoDaily.Get(request.UserPropertyId);
        }
    }
}
