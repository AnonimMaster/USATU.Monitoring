using System.Net.NetworkInformation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using USATU.Monitoring.Core.Domains.Tasks.Services;
using USATU.Monitoring.Core.Domains.Users.Services;

namespace USATU.Monitoring.Core
{
    public static class Bootstraps
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddHttpClient<ITaskService, TaskService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddFluentValidation().AddValidatorsFromAssembly(typeof(UserService).Assembly);

            return services;
        }

    }
}
