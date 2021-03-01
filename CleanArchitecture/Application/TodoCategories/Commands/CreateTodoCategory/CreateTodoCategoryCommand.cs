using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace TodoList.Application.TodoCategories.Commands.CreateTodoCategory
{
    public class CreateTodoCategoryCommand : IRequest<int>
    {
        public string CategoryTitle { get; set; }
    }
    public class CreateTodoCategoryCommandHandler : IRequestHandler<CreateTodoCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;
        public CreateTodoCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new TodoCategory();
            entity.CategoryTitle = request.CategoryTitle;
            _context.TodoCategories.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
