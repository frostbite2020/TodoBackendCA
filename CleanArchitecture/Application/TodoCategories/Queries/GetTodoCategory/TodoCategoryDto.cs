﻿using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.TodoCategories.Queries.GetTodoCategory
{
    public class TodoCategoryDto : IMapFrom<TodoCategory>
    {
        public TodoCategoryDto()
        {
            Lists = new List<TodoItemDto>();
        }

        public int Id { get; set; }

        public string CategoryTitle { get; set; }

        public IList<TodoItemDto> Lists { get; set; }
    }
}
