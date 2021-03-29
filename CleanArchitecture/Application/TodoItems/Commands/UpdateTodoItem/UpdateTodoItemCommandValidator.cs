/*using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.ActivityTitle)
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(200).WithMessage("Activity Title must not exceed 200 characters");
        }
    }
}
*/