using FluentValidation;
using USATU.Monitoring.Core.Domains.Users.Repositories;

namespace USATU.Monitoring.Core.Domains.Users.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Login).NotEmpty().WithName("Логин").WithMessage("не может быть пустым");
            RuleFor(x => x.Login).MaximumLength(32).WithName("Логин").WithMessage("превышает 32 символа.");
            RuleFor(x => x.Password).NotEmpty().WithName("Пароль").WithMessage("не может быть пустым");
            RuleFor(x => x.Password).MaximumLength(64).WithName("Пароль").WithMessage("превышает 64 символа.");
            RuleFor(x => x.Login).MustAsync(async (login, cancancellation) =>
            {
                var exists = await userRepository.IsUserWithLogin(login);
                return !exists;
            }).WithName("Логин").WithMessage("Пользователь с таким логином уже существует");
        }

    }
}