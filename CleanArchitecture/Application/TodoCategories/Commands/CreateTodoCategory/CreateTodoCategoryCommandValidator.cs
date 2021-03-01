using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(200).WithMessage("Category title must not exceed 200 characters")
                .MustAsync(BeUniqueCategoryTitle).WithMessage("The spesified title alredy exist");
        }
        public async Task<bool> BeUniqueCategoryTitle(string categoryTitle, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories
                .AllAsync(v => v.CategoryTitle != categoryTitle);
        }

    }
}
