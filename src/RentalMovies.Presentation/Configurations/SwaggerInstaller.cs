using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RentalMovies.Presentation.Configurations
{
    public static class SwaggerInstaller
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "Ever Orellana",
                Email = "ever1509@gmail.com",
                Url = new Uri("https://github.com/ever1509")
            };

            var license = new OpenApiLicense()
            {
                Name = "Rental Movies License App",
                Url = new Uri("https://www.rentalmovies.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Rental Movies API",
                Description = "API about a process  to rental movies in order to handle stocks, movies and handle the process to return one",
                TermsOfService = new Uri("https://www.rentalmovies.com/terms/"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("RentalMoviesAPI", info);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //Adding configuration of jwt for swagger UI ----------------------------------

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }},
                        new List<string>()
                    }
                });
                //----------------------------------------
            });

            return services;
        }
    }
}
