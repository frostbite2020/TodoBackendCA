using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(200).WithMessage("Title must not be exceed 200 characters")
                .MustAsync(BeUniqueCategoryTitle).WithMessage("The specified category title alredy exists.");
        }
        
        public async Task<bool> BeUniqueCategoryTitle(UpdateTodoCategoryCommand model, string categoryTitle, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories
                .Where(v => v.Id == model.Id)
                .AllAsync(v => v.CategoryTitle != categoryTitle);
        } 
    }
}
