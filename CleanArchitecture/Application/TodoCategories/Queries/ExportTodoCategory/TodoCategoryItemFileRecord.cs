using Application.Common.Mappings;
using Domain.Entities;

namespace Application.TodoCategories.Queries.ExportTodoCategory
{
    public class TodoCategoryItemFileRecord : IMapFrom<TodoItem>
    {
        public string ActivityTitle { get; set; }

        public bool Done { get; set; }
    }
}