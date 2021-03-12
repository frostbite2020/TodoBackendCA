using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoCategories.Queries.GetTodoCategory
{
    public class TodoItemDto : IMapFrom<TodoItem>
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string ActivityTitle { get; set; }

        public bool Done { get; set; }

        public int Priority { get; set; }

        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TodoItem, TodoItemDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
