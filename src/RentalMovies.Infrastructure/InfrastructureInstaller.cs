using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Infrastructure.Data;
using RentalMovies.Infrastructure.Identity;

namespace RentalMovies.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RentalMoviesDbContext>(options =>
                {
                    options.UseInMemoryDatabase("RentalMoviesDb");
                });
            }
            else
            {
                services.AddDbContext<RentalMoviesDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("RentalMoviesDbConnection"),
                        b => b.MigrationsAssembly((typeof(RentalMoviesDbContext).Assembly.FullName)));
                });
            }

            services.AddScoped<IRentalMoviesDbContext>(provider => provider.GetService<RentalMoviesDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RentalMoviesDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
