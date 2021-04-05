using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.UserModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserProperty> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _context.UserProps.SingleOrDefaultAsync(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<IList<UserModel>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.UserProps
                .AsNoTracking()
                .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.FirstName)
                .ToListAsync(cancellationToken);
        }

        public async Task<UserModel> GetById(int id)
        {
            var user = await _context.UserProps.FindAsync(id);
            var model = _mapper.Map<UserModel>(user);
            return model;
        }

        public async Task<UserProperty> Create(UserProperty user, string password, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.UserProps.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("Value cannot be empty or whitespace only string.", " password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await _context.UserProps.FindAsync(id);
            if (user != null)
            {
                _context.UserProps.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return user.Id;
        }

        public async Task<UpdateModel> Update(UserProperty userParam, CancellationToken cancellationToken, string password = null)
        {
            var user = await _context.UserProps.FindAsync(userParam.Id);

            if (user == null)
                throw new NotFoundException("User not found");

            //input change
            user.Username = userParam.Username;
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Email = userParam.Email;
            user.PhoneNumber = userParam.PhoneNumber;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.UserProps.Update(user);
            await _context.SaveChangesAsync(cancellationToken);

            var model = new UpdateModel
            {
                FirstName = userParam.FirstName,
                LastName = userParam.LastName,
                Username = userParam.Username,
                Email = userParam.Email,
                PhoneNumber = userParam.PhoneNumber
            };
            return model;
        }
    }
}

