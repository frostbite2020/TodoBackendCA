﻿using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UserUpdate
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        private readonly IApplicationDbContext _context;

        public UserUpdateCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("This field is required")
                .MaximumLength(200).WithMessage("Username must not exceed 200 characters.")
                .MustAsync(BeUniqueUsername).WithMessage("Username is alredy exist!");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Firstname must not exceed 200 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Lastname must not exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This field is required!")
                .MaximumLength(200).WithMessage("Email must not exceed 200 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("This field is required!");
        }

        public async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            return await _context.UserProps
                .AllAsync(x => x.Username != username);
        }
    }
}
