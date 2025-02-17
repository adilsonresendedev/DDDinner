using DDDinner.Infrastructure.Authentication;

namespace DDDinner.Api.Infra
{
    public static class OptionsConfiguration
    {
        public static IServiceCollection ConfigureCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            return services;
        }
    }
}
