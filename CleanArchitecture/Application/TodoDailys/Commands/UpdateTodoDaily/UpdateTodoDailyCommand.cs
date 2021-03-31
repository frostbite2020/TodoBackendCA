using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.UpdateTodoDaily
{
    public class UpdateTodoDailyCommand : IRequest<bool>
    {
        public int todoDailyId { get; set; }
    }
    public class UpdateTodoDailyCommandHandler : IRequestHandler<UpdateTodoDailyCommand, bool>
    {
        private ITodoDaily _todoDaily;
        public UpdateTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }
        public async Task<bool> Handle(UpdateTodoDailyCommand request, CancellationToken cancellationToken)
        {
            return await _todoDaily.Update(request.todoDailyId, cancellationToken);
        }
    }
}
