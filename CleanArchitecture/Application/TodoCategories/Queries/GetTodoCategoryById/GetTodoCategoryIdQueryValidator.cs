/*using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.TodoCategories.Queries.GetTodoCategoryById
{
    public class GetTodoCategoryIdQueryValidator : AbstractValidator<GetTodoCategoryIdQuery>
    {
        private IApplicationDbContext _context;
        public GetTodoCategoryIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .MustAsync(ValidId).WithMessage("value u input is not valid");
        }

        public async Task<bool> ValidId(int id, CancellationToken cancellationToken)
        {
            return await _context.TodoCategories.AllAsync(x => x.Id == id);
        }
    }
}
*/