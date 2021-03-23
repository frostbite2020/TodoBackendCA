using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TodoCategories.Queries.GetTodoCategory;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Queries.GetTodoItemById
{
    public class GetTodoItemIdQuery : IRequest<TodoItemDto>
    {
        public int Id { get; set; }
    }
    public class GetTodoItemIdQueryHandler : IRequestHandler<GetTodoItemIdQuery, TodoItemDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetTodoItemIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TodoItemDto> Handle(GetTodoItemIdQuery query, CancellationToken cancellationToken)
        {
            if(_context.TodoItems.Any(x => x.Id == query.Id))
            {
                return await _context.TodoItems
                    .ProjectTo<TodoItemDto>(_mapper.ConfigurationProvider)
                    .Where(x => x.Id == query.Id).FirstOrDefaultAsync();
            }

            throw new NotFoundException(nameof(TodoItem), query.Id);
        }
    }
}
