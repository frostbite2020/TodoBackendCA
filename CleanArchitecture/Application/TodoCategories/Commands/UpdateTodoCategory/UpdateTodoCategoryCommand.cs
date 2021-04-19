using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoCategories.Commands.UpdateTodoCategory
{
    public class UpdateTodoCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class UpdateTodocategoryCommandHandler : IRequestHandler<UpdateTodoCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTodocategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoCategories.FindAsync(request.Id);
            
            if(entity == null)
            {
                throw new NotFoundException(nameof(TodoCategory), request.Id);
            }
            entity.Id = request.Id;
            entity.CategoryTitle = request.CategoryTitle;

            var titleValidation = _context.TodoCategories
                .Where(x => x.UserPropertyId == entity.UserPropertyId)
                .Any(x => x.CategoryTitle == request.CategoryTitle);

            if (titleValidation)
                throw new AppException();

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
