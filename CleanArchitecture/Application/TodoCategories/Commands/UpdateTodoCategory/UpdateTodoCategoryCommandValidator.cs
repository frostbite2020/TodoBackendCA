using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoCategories.Commands.UpdateTodoCategory
{
    public class UpdateTodoCategoryCommandValidator : AbstractValidator<UpdateTodoCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryTitle)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateTodoCategoryCommand model, string categoryTitle, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.CategoryTitle != categoryTitle);
        }
    }
}
