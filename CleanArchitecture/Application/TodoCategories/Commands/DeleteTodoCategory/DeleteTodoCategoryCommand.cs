using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoCategories.Commands.DeleteTodoCategory
{
    public class DeleteTodoCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }
    public class DeleteTodoCategoryCommandHandler : IRequestHandler<DeleteTodoCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoCategories
                .Where(v => v.Id == request.Id)
                .SingleOrDefaultAsync();

            if(entity == null)
            {
                throw new NotFoundException(nameof(TodoCategory), request.Id);
            }

            _context.TodoCategories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
