using Application.TodoLists.Commands.CreateTodoList;
using Domain.Enums;
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

            RuleFor(x => x.Priority)
                .Must(BeInRange).WithMessage("Priority u input is not exist");
        }
        private bool BeInRange(PriorityLevel priority)
        {
            if (priority == PriorityLevel.None
                || priority == PriorityLevel.Low
                || priority == PriorityLevel.Medium
                || priority == PriorityLevel.High)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
