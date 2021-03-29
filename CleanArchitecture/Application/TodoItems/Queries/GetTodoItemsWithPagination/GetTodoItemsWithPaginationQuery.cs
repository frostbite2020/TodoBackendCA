using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.TodoCategories.Queries.GetTodoCategory;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class GetTodoItemsWithPaginationQuery : IRequest<TodoItemVm>
    {
        public int CategoryId { get; set; }
        public string searchByTitle { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SortingProperties Sorting { get; set; }
        public PriorityLevel FilterByPriority { get; set; }
    }

    public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemsWithPaginationQuery, TodoItemVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TodoItemVm> Handle(GetTodoItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var todoItem = from s in _context.TodoItems
                           select s;

            //Filter By Priority
            switch (request.FilterByPriority)
            {
                case 0:
                    todoItem = from s in _context.TodoItems
                               select s;
                    break;
                case PriorityLevel.None:
                    todoItem = todoItem.Where(s => s.Priority.Equals(PriorityLevel.None));
                    break;
                case PriorityLevel.Low:
                    todoItem = todoItem.Where(s => s.Priority.Equals(PriorityLevel.Low));
                    break;
                case PriorityLevel.Medium:
                    todoItem = todoItem.Where(s => s.Priority.Equals(PriorityLevel.Medium));
                    break;
                case PriorityLevel.High:
                    todoItem = todoItem.Where(s => s.Priority.Equals(PriorityLevel.High));
                    break;
                default:
                    todoItem = null;
                    break;
            }

            //Sorting
            switch (request.Sorting)
            {
                case 0:
                    todoItem = todoItem.OrderBy(s => s.CategoryId);
                    break;
                case SortingProperties.ByName:
                    todoItem = todoItem.OrderBy(s => s.ActivityTitle);
                    break;
                case SortingProperties.DescentByName:
                    todoItem = todoItem.OrderByDescending(s => s.ActivityTitle);
                    break;
                case SortingProperties.ByPriority:
                    todoItem = todoItem.OrderBy(s => s.Priority);
                    break;
                default:
                    todoItem = null;
                    break;
            }

            if (!string.IsNullOrEmpty(request.searchByTitle))
            {
                todoItem = todoItem.Where(o => o.ActivityTitle.Contains(request.searchByTitle));
            }

            //Return
            return new TodoItemVm
            {
                PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                    .Cast<PriorityLevel>()
                    .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                    .ToList(),

                Sortings = Enum.GetValues(typeof(SortingProperties))
                    .Cast<SortingProperties>()
                    .Select(p => new SortingPropertiesDto { Value = (int)p, Name = p.ToString()})
                    .ToList(),

                TodoItems = await todoItem
                    .Where(x => x.CategoryId == request.CategoryId)
                    .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize)
            };
        }
    }
}
