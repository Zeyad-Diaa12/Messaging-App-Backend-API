using Microsoft.EntityFrameworkCore;
using SocialApi.Data;
using SocialApi.Interfaces;
using SocialApi.Services;

namespace SocialApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(
                    options =>
                    {
                        options.UseSqlite(config.GetConnectionString("conn"));
                    }
                );

            services.AddScoped<ITokenService, TokenService>();

            services.AddCors(
                    options =>
                    {
                        options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                                }
                            );
                    }
                );

            return services;
        }
    }
}
