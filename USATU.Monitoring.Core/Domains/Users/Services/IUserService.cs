using System.Collections.Generic;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Users.Repositories;

namespace USATU.Monitoring.Core.Domains.Users.Services
{
    public interface IUserService
    {
        public Task<User> GetUser(string id);
        public Task CreateUser(User user);
        public Task<List<User>> GetAllUsers();
        public Task UpdateUser(User user);
        public Task DeleteUser(string id);

    }
}