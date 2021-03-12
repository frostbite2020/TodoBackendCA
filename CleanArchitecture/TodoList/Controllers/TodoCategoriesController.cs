using Application.TodoCategories.Commands.DeleteTodoCategory;
using Application.TodoCategories.Commands.UpdateTodoCategory;
using Application.TodoCategories.Queries.ExportTodoCategory;
using Application.TodoCategories.Queries.GetTodoCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoList.Application.TodoCategories.Commands.CreateTodoCategory;

namespace TodoList.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TodoCategoriesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }
        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportTodoCategoriesQuery { CategoryId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
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
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoCategoryCommand { Id = id });
            return NoContent();
        }
    }
}
