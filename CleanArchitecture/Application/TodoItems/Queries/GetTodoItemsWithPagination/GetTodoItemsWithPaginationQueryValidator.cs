using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.TodoItems.Queries.GetTodoItemsWithPagination
{
    public class GetTodoItemsWithPaginationQueryValidator : AbstractValidator<GetTodoItemsWithPaginationQuery>
    {
        public GetTodoItemsWithPaginationQueryValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotNull()
                .NotEmpty().WithMessage("This field is required");

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than 1 or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");

            /*RuleFor(x => x.FilterByPriority)
                .Must(BeInRange).WithMessage("Priority u input is not in exist");

            RuleFor(x => x.Sorting)
                .Must(SortingRule).WithMessage("That kind of sorting is not available");*/
        }

        private bool BeInRange(PriorityLevel priority)
        {
            if (priority == 0
                || priority == PriorityLevel.None
                || priority == PriorityLevel.Low
                || priority == PriorityLevel.Medium
                || priority == PriorityLevel.High) return true;

            return false;
        }

        public bool SortingRule(SortingProperties sorting)
        {
            if (sorting == 0
                || sorting == SortingProperties.ByName
                || sorting == SortingProperties.DescentByName
                || sorting == SortingProperties.ByPriority) return true;

            return false;
        }
    }
}
