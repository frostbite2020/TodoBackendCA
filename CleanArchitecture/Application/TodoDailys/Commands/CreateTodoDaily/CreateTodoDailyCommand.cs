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
    public class CreateTodoDailyCommand : IRequest<int>
    {
        public int userPropertyId { get; set; }
        public string TodoDailyActivity { get; set; }
    }
    public class CreateTodoDailyCommandHandler : IRequestHandler<CreateTodoDailyCommand, int>
    {
        private readonly ITodoDaily _todoDaily;
        public CreateTodoDailyCommandHandler(ITodoDaily todoDaily)
        {
            _todoDaily = todoDaily;
        }
        public async Task<int> Handle(CreateTodoDailyCommand request, CancellationToken cancellationToken)
        {
            var asset = new TodoDaily
            {
                TodoDailyActivity = request.TodoDailyActivity,
                Check = false,
                MadeSince = DateTime.Now,
                MadeUntil = DateTime.Now.AddDays(1),
                UserPropertyId = request.userPropertyId
            };

            return await _todoDaily.Add(asset, cancellationToken);
        }
    }
}
