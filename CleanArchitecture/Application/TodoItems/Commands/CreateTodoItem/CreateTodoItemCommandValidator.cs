using Application.TodoLists.Commands.CreateTodoList;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.ActivityTitle)
                .MaximumLength(200).WithMessage("Activity title must not exceed 200 characters")
                .NotEmpty().WithMessage("This field is required");
        }
    }
}
