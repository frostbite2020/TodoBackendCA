using Application.Common.Interfaces;
using Application.TodoDailys.Queries.GetAllTodoDailys;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoDailys.Commands.CreateTodoDaily
{
    public class CreateTodoDailyCommand : IRequest<TodoDailyDto>
    {
        public int userPropertyId { get; set; }
        public string TodoDailyActivity { get; set; }
    }
    public class CreateTodoDailyCommandHandler : IRequestHandler<CreateTodoDailyCommand, TodoDailyDto>
    {
        private ITodoDaily _todoDaily;
        public CreateTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }
        public async Task<TodoDailyDto> Handle(CreateTodoDailyCommand request, CancellationToken cancellationToken)
        {
            var asset = new TodoDaily
            {
                TodoDailyActivity = request.TodoDailyActivity,
                Check = false,
                MadeSince = DateTime.UtcNow,
                MadeUntil = DateTime.UtcNow.AddDays(1),
                UserPropertyId = request.userPropertyId
            };

            return await _todoDaily.Add(asset, cancellationToken);
        }
    }
}
