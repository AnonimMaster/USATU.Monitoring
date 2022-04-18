using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using USATU.Monitoring.Core.Domains.Users;
using USATU.Monitoring.Core.Domains.Users.Services;
using USATU.Monitoring.Web.Controllers.Users.Dto;

namespace USATU.Monitoring.Web.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<UserDto> GetUser(string userId)
        {
            var model = await _userService.GetUser(userId);

            return new UserDto()
            {
                Id = model.Id,
                Login = model.Login,
                Password = model.Password
            };
        }


        [HttpGet]
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return users.Select(i => new UserDto()
            {
                Id = i.Id,
                Login = i.Login,
                Password = i.Password
            }).ToList();
        }

        [HttpPost]
        public Task Create(UserCreateDto model)
        {
            return _userService.CreateUser(new User()
            {
                Login = model.Login,
                Password = model.Password
            });
        }

        [HttpPut("{userId}")]
        public Task Update(string userId, UserUpdateDto model)
        {
            return _userService.UpdateUser(new User()
            {
                Id = userId,
                Login = model.Login,
                Password = model.Password
            });
        }

        [HttpDelete("{userId}")]
        public Task Delete(string userId)
        {
            return _userService.DeleteUser(userId);
        }

    }
}
