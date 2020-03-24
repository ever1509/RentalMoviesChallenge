using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RentalMovies.Application.Behaviours;
using RentalMovies.Application.Mappings;
using RentalMovies.Application.Movies.AddMovieLike;
using RentalMovies.Application.Movies.CreateMovie;
using RentalMovies.Application.Movies.DeleteMovie;
using RentalMovies.Application.Movies.GetAllMoviesList;
using RentalMovies.Application.Movies.GetMovieDetail;
using RentalMovies.Application.Movies.UpdateMovie;
using RentalMovies.Application.RentalMovies.CreateRentalMovie;
using RentalMovies.Application.RentalMovies.UpdateRentalMovie;

namespace RentalMovies.API.Configurations
{
    public static class ApplicationSetup
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var MoviesMapperCfg = new MapperConfiguration(
                cfg =>
                {
                    cfg.AddProfile<RentalMoviesMapperConfiguration>();
                }

            );
            var mapperMovies = MoviesMapperCfg.CreateMapper();

            services.AddSingleton(mapperMovies);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddMediatR(typeof(CreateMovieCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateMovieCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(DeleteMovieCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(AddMovieLikeCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetMovieDetailQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetAllMoviesListQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(RentalMovieCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ReturnMovieCommand).GetTypeInfo().Assembly);

            return services;
        }
    }
}
