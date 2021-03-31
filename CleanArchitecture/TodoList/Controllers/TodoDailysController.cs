using Application.Common.Exceptions;
using Application.TodoDailys.Commands.CreateTodoDaily;
using Application.TodoDailys.Commands.DeleteTodoDaily;
using Application.TodoDailys.Commands.DeleteTodoDailyHistory;
using Application.TodoDailys.Commands.UncheckTodoDaily;
using Application.TodoDailys.Commands.UpdateTodoDaily;
using Application.TodoDailys.Queries.GetAllTodoDailys;
using Application.TodoDailys.Queries.GetTodoDailyHistories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [Route("todo-daily")]
    public class TodoDailysController : ApiControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<IList<TodoDailyDto>> Get(int userId)
        {
            var query = new GetAllTodoDailyQuery();
            query.UserPropertyId = userId;
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> Add(CreateTodoDailyCommand command)
        {
            return await Mediator.Send(command);
        }
        
        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await Mediator.Send(new DeleteTodoDailyCommand{ TodoDailyId = id });
        }

        [HttpPut("check/{id}")]
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

        [HttpGet("history")]
        public async Task<TodoDailyHistoryVm> GetHistory([FromQuery] GetTodoDailyHistoryQueries query)
        {
            return await Mediator.Send(query);
        }

        [HttpDelete("history/{id}")]
        public async Task<int> DeleteHistory(int id)
        {
            return await Mediator.Send(new DeleteTodoDailyHistoryCommand { TodoDailyHistoryId = id });
        }
    }
}
