using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using USATU.Monitoring.Core;
using USATU.Monitoring.Core.Domains.Tasks.Repositories;
using USATU.Monitoring.Core.Domains.Users.Repositories;
using USATU.Monitoring.Data.Tasks.Repositories;
using USATU.Monitoring.Data.Users.Repositories;

namespace USATU.Monitoring.Data
{
    public static class Bootstraps
    {
        public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Database")));
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            return services;
        }
    }

}