using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoItems.Commands.UpdateTodoItemDetail
{
    public class UpdateTodoItemDetailCommandValidator : AbstractValidator<UpdateTodoItemDetailCommand>
    {
        public UpdateTodoItemDetailCommandValidator()
        {
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
