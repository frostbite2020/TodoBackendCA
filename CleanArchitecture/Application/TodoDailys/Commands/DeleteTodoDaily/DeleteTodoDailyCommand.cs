using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.DeleteTodoDaily
{
    public class DeleteTodoDailyCommand : IRequest<int>
    {
        public int TodoDailyId { get; set; }
    }

    public class DeleteTodoDailyCommandHandler : IRequestHandler<DeleteTodoDailyCommand, int>
    {
        private ITodoDaily _todoDaily;
        public DeleteTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }

        public async Task<int> Handle(DeleteTodoDailyCommand request, CancellationToken cancellationToken)
        {
            return await _todoDaily.Delete(request.TodoDailyId, cancellationToken);
        }
    }
}
