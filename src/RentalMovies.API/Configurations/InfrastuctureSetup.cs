using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalMovies.Infrastucture.Mail;
using RentalMovies.Infrastucture.Mail.EmailConfiguration;

namespace RentalMovies.API.Configurations
{
    public static class InfrastuctureSetup
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddSingleton<IEmailConfiguration>(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddTransient<IEmailService, EmailService>();

            return services;
        }


    }
}
