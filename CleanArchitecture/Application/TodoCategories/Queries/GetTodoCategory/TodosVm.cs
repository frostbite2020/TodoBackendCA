using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoCategories.Queries.GetTodoCategory
{
    public class TodosVm
    {
        public IList<PriorityLevelDto> PriorityLevels { get; set; }
        public IList<TodoCategoryDto> Categories { get; set; }
    }
}
