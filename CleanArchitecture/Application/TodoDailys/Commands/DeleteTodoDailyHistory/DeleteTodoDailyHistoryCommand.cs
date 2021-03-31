using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.DeleteTodoDailyHistory
{
    public class DeleteTodoDailyHistoryCommand : IRequest<int>
    {
        public int TodoDailyHistoryId { get; set; }
    }
    
    public class DeleteTodoDailyHistoryCommandHandler : IRequestHandler<DeleteTodoDailyHistoryCommand, int>
    {
        private ITodoDaily _todoDaily;
        public DeleteTodoDailyHistoryCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }

        public async Task<int> Handle(DeleteTodoDailyHistoryCommand request, CancellationToken cancellationToken)
        {
            return await _todoDaily.DeleteHistory(request.TodoDailyHistoryId, cancellationToken);
        }
    }
}
