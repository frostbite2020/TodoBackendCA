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
        public bool CheckStatus { get; set; }
    }
    public class UpdateTodoDailyCommandHandler : IRequestHandler<UpdateTodoDailyCommand, bool>
    {
        private readonly ITodoDaily _todoDaily;
        public UpdateTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }
        public async Task<bool> Handle(UpdateTodoDailyCommand request, CancellationToken cancellationToken)
        {
            var asset = new UpdateTodoDailyDto
            {
                Id = request.todoDailyId,
                CheckStatus = request.CheckStatus
            };
            return await _todoDaily.Update(asset, cancellationToken);
        }
    }
}
