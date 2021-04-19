using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TodoList.Application.TodoCategories.Commands.CreateTodoCategory
{
    public class CreateTodoCategoryCommandValidator : AbstractValidator<CreateTodoCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.CategoryTitle)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.UserPropertyId)
                .NotEmpty().WithMessage("Value cannot be null")
                .MustAsync(HaveUserId).WithMessage("Cant find user id");
        }

        public async Task<bool> HaveUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories
                .AnyAsync(x => x.UserPropertyId == userId);
        }

    }
}
