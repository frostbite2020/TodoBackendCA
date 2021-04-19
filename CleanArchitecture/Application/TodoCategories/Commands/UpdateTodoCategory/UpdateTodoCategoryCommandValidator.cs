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
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");
        }
    }
}
