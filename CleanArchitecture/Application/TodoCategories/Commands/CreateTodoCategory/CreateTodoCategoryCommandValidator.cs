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
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string categoryTitle, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories
                .AllAsync(l => l.CategoryTitle != categoryTitle);
        }

    }
}
