using Application.Common.Models;
using Application.TodoCategories.Queries.GetTodoCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class TodoItemVm
    {
        public IList<SortingPropertiesDto> Sortings { get; set; }
        public IList<PriorityLevelDto> PriorityLevels { get; set; }
        public PaginatedList<TodoItemDto> TodoItems { get; set; }
    }
}
