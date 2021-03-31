using Application.Common.Exceptions;
using Application.TodoDailys.Commands.CreateTodoDaily;
using Application.TodoDailys.Commands.UncheckTodoDaily;
using Application.TodoDailys.Commands.UpdateTodoDaily;
using Application.TodoDailys.Queries.GetAllTodoDailys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [Route("todo-daily")]
    public class TodoDailysController : ApiControllerBase
    {
        [HttpPost]
        public async Task<TodoDailyDto> Add(CreateTodoDailyCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Update(int id, UpdateTodoDailyCommand command)
        {
            if (command.todoDailyId != id)
                throw new NotFoundException();

            return await Mediator.Send(command);
        }

        [HttpPut("uncheck/{id}")]
        public async Task<ActionResult<bool>> Uncheck(int id, UncheckTodoDailyCommand command)
        {
            if (command.TodoDailyHistoryId != id)
                throw new NotFoundException();

            return await Mediator.Send(command);
        }
    }
}
