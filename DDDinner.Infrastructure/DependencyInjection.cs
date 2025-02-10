using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Application.Common.Persistence;
using DDDinner.Application.Common.Services;
using DDDinner.Infrastructure.Authentication;
using DDDinner.Infrastructure.Persistence;
using DDDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordUtility, PasswordUtility>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            return services;
        }
    }
}
