using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RentalMovies.API.Configurations;

namespace RentalMovies.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }    

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowRentalMoviesOrigin",
                    builder=>builder.WithOrigins("https://localhost:44362"));
            });

            services.AddControllers();

            var contact = new OpenApiContact()
            {
                Name = "Ever Edgardo Orellana Perez",
                Email = "ever1509@gmail.com",
                Url = new Uri("https://github.com/ever1509")
            };
            var license = new OpenApiLicense()
            {
                Name = "My License Rental Movies",
                Url = new Uri("http://www.rentalmoviestest.com")
            };
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Rental Movies API",
                Description = "Swagger Rental Movies API in order to do a technical recruitment process",
                TermsOfService = new Uri("http://www.rentalmoviesterms.com"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("RentalMoviesAPI", info);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 
            });

            services.AddDatabaseSetup(Configuration);

            services.AddApplication();

            services.AddInfrastucture(Configuration, Environment);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowRentalMoviesOrigin");

            app.UseAuthorization();

            //app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/RentalMoviesAPI/swagger.json", "Swagger Rental Movies API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
