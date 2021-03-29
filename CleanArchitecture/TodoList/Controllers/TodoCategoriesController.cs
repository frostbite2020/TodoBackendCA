using Application.Common.Models.UserModels;
using Application.TodoCategories.Commands.DeleteTodoCategory;
using Application.TodoCategories.Commands.UpdateTodoCategory;
using Application.TodoCategories.Queries.ExportTodoCategory;
using Application.TodoCategories.Queries.GetTodoCategory;
using Application.TodoCategories.Queries.GetTodoCategoryById;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace TodoList.Controllers
{
    [Authorize]
    [Route("category")]
    public class TodoCategoriesController : ApiControllerBase
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<TodosVm>> Get(int userId)
        {
            var currentUserId = int.Parse(User.Identity.Name);
            var query = new GetTodosQuery();
            query.CurrentUserId = userId;
            if (currentUserId != query.CurrentUserId && !User.IsInRole(Role.Admin))
                return Forbid();
            return await Mediator.Send(query);
        }

        [HttpGet("category-by-id/{id}")]
        public async Task<ActionResult<TodoCategoryDto>> GetById(int id)
        {
            var query = new GetTodoCategoryIdQuery();
            query.Id = id;
            if (id != query.Id)
            {
                return BadRequest();
            }
            return await Mediator.Send(query);
        }
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoCategoryCommand command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTodoCategoryCommand command)
        {
            
            if(id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoCategoryCommand { Id = id });
            return NoContent();
        }
    }
}
