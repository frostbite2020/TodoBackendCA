﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.UserModels;
using Application.Common.Models.UserModels.Helpers;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserManagements.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<UserLoginSuccessDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginSuccessDto>
    {
        private readonly IUserService _userService;
        private readonly AppSettingUsers _appSettings;

        public UserLoginCommandHandler(IUserService userService, IOptions<AppSettingUsers> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        public async Task<UserLoginSuccessDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.Authenticate(request.Username, request.Password);

            if(user == null)
            {
                throw new NotFoundException("Username or Password is incorrect");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var entity = new UserLoginSuccessDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Token = tokenString
            };
            // return basic user info and authentication token
            return entity;
        }
    }
}
