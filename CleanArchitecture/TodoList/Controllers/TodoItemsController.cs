using Application.Common.Models;
using Application.TodoCategories.Queries.GetTodoCategory;
using Application.TodoItems.Commands.DeleteTodoItem;
using Application.TodoItems.Commands.UpdateTodoItem;
using Application.TodoItems.Commands.UpdateTodoItemDetail;
using Application.TodoItems.Queries.GetTodoItemById;
using Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Application.TodoLists.Commands.CreateTodoList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TodoList.Controllers
{
    [Authorize(Roles = "Admin, User")]
    [Route("item")]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TodoItemVm>> GetTodoItemsWithPagination([FromQuery]GetTodoItemsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<TodoItemDto> GetTodoItemsById(int id)
        {
            var query = new GetTodoItemIdQuery();
            query.Id = id;
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoItemCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut("update-item-details/{id}")]
        public async Task<ActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id });

            return NoContent();
        }
    }
}
