using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Queries.GetTodoDailyHistories
{
    public class GetTodoDailyHistoryQueries : IRequest<TodoDailyHistoryVm>
    {
        public int UserPropertyId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetTodoDailyHistoryQueriesHandler : IRequestHandler<GetTodoDailyHistoryQueries, TodoDailyHistoryVm>
    {
        private ITodoDaily _todoDaily;

        public GetTodoDailyHistoryQueriesHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }

        public async Task<TodoDailyHistoryVm> Handle(GetTodoDailyHistoryQueries request, CancellationToken cancellationToken)
        {
            return await _todoDaily.GetTodoDailyHistory(request.UserPropertyId, request.PageNumber, request.PageSize);
        }
    }
}
