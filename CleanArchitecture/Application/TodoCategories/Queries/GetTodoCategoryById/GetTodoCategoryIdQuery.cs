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

namespace Application.TodoCategories.Queries.GetTodoCategoryById
{
    public class GetTodoCategoryIdQuery : IRequest<TodoCategoryDto>
    {
        public int Id { get; set; }
    }
    public class GetTodoCategoryIdQueryHandler : IRequestHandler<GetTodoCategoryIdQuery, TodoCategoryDto>
    {
        private IApplicationDbContext _context;
        private IMapper _mapper;
        public GetTodoCategoryIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TodoCategoryDto> Handle(GetTodoCategoryIdQuery request, CancellationToken cancellationToken)
        {
            if(_context.TodoCategories.Any(x => x.Id == request.Id))
            {
                var asset = await _context.TodoCategories
                .ProjectTo<TodoCategoryDto>(_mapper.ConfigurationProvider)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

                return new TodoCategoryDto
                {
                    Id = request.Id,
                    CategoryTitle = asset.CategoryTitle,
                    Lists = asset.Lists
                };
            }
            
            throw new NotFoundException(nameof(TodoCategory), request.Id);
        }
    }
}
