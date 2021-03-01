using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoCategories.Queries.GetTodoCategory
{
    public class TodoCategoryDto : IMapFrom<TodoCategory>
    {
        public TodoCategoryDto()
        {
            Items = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string CategoryTitle { get; set; }


        public IList<TodoItemDto> Items { get; set; }
    }
}
