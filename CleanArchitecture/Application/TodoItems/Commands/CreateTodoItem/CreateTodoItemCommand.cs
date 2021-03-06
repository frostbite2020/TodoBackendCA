﻿using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoItemCommand : IRequest<int>
    {
        public int CategoryId { get; set; }

        public string ActivityTitle { get; set; }
        public string Note { get; set; }
        public PriorityLevel Priority { get; set; }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoItem
            {
                CategoryId = request.CategoryId,
                ActivityTitle = request.ActivityTitle,
                Note = request.Note,
                Priority = request.Priority,
                Done = false
            };

            _context.TodoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
