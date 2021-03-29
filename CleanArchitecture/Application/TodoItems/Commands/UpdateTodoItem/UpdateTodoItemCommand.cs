using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest
    {
        public int Id { get; set; }
        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        private IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);
            
            if(entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }
            entity.Done = request.Done;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
