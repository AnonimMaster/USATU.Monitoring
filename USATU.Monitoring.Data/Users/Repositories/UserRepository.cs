using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using USATU.Monitoring.Core.Domains.Users;
using USATU.Monitoring.Core.Domains.Users.Repositories;
using USATU.Monitoring.Core.Exceptions;
using System.Linq;

namespace USATU.Monitoring.Data.Users.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string id)
        {
            var entityUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

            if (entityUser == null)
            {
                throw new ObjectNotFoundException($"Пользователя с Id = {id} не существует.");
            }

            return new User()
            {
                Id = entityUser.Id,
                Login = entityUser.Login,
                Password = entityUser.Password
            };
        }

        public Task CreateUser(User user)
        {
            var entityUser = new UserDbModel()
            {
                Id = Guid.NewGuid().ToString(),
                Login = user.Login,
                Password = user.Password
            };

            return _context.Users.AddAsync(entityUser).AsTask();
        }

        public Task<List<User>> GetAllUsers()
        {
            return _context.Users
                .AsNoTracking()
                .Select(i => new User()
                {
                    Id = i.Id,
                    Login = i.Login,
                    Password = i.Password
                }).ToListAsync();
        }

        public async Task UpdateUser(User user)
        {
            var entityUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == user.Id);

            if (entityUser == null)
            {
                throw new ObjectNotFoundException($"Пользователя с Id = {user.Id} не существует.");
            }

            entityUser.Login = user.Login;
            entityUser.Password = user.Password;
        }

        public async Task DeleteUser(string id)
        {
            var entityUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);

            if (entityUser == null)
            {
                throw new ObjectNotFoundException($"Пользователя с Id = {id} не существует.");
            }

            _context.Remove(entityUser);
        }

        public Task<bool> IsUserExists(string id)
        {
            return _context.Users.AnyAsync(i => i.Id == id);
        }

        public Task<bool> IsUserWithLogin(string login)
        {
            return _context.Users.AnyAsync(i => i.Login == login);
        }

    }
}