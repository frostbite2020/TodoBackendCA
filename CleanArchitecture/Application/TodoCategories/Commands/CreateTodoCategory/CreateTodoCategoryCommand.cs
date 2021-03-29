using Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Application.Common.Exceptions;

namespace TodoList.Application.TodoCategories.Commands.CreateTodoCategory
{
    public class CreateTodoCategoryCommand : IRequest<int>
    {
        public string CategoryTitle { get; set; }
        public int UserPropertyId { get; set; }
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
            var entity = new TodoCategory
            {
                CategoryTitle = request.CategoryTitle,
                UserPropertyId = request.UserPropertyId,
            };

            var titleValidation = _context.TodoCategories
                .Where(x => x.UserPropertyId == request.UserPropertyId)
                .Any(x => x.CategoryTitle == request.CategoryTitle);

            if (titleValidation)
                throw new AppException();

            _context.TodoCategories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
