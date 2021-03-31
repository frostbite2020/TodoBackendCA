using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.UncheckTodoDaily
{
    public class UncheckTodoDailyCommand : IRequest<bool>
    {
        public int TodoDailyHistoryId { get; set; }
    }

    public class UncheckTodoDailyCommandHandler : IRequestHandler<UncheckTodoDailyCommand, bool>
    {
        private ITodoDaily _todoDaily;
        public UncheckTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }

        public async Task<bool> Handle(UncheckTodoDailyCommand request, CancellationToken cancellationToken)
        {
            return await _todoDaily.UnceklistHistory(request.TodoDailyHistoryId, cancellationToken);
        }
    }
}
