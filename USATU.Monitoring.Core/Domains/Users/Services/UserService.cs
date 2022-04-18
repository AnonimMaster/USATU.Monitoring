using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using USATU.Monitoring.Core.Domains.Users.Repositories;

namespace USATU.Monitoring.Core.Domains.Users.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _userValidator;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IValidator<User> userValidator, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _unitOfWork = unitOfWork;
        }

        public Task<User> GetUser(string id)
        {
            return _userRepository.GetUser(id);
        }

        public async Task CreateUser(User user)
        {
            await _userValidator.ValidateAndThrowAsync(user);
            await _userRepository.CreateUser(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<List<User>> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public async Task UpdateUser(User user)
        {
            await _userValidator.ValidateAndThrowAsync(user);
            await _userRepository.UpdateUser(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteUser(string id)
        {
            var isUserExists = await _userRepository.IsUserExists(id);

            if (!isUserExists)
            {
                throw new ValidationException($"Пользователя с Id = {id} нет.");
            }

            await _userRepository.DeleteUser(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}