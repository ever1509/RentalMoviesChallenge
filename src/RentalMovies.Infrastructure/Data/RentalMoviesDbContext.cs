using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Domain.Entities;
using RentalMovies.Infrastructure.Identity;

namespace RentalMovies.Infrastructure.Data
{
    public class RentalMoviesDbContext:IdentityDbContext<ApplicationUser>,IRentalMoviesDbContext
    {
        private IDbContextTransaction _currentTransaction;
        public RentalMoviesDbContext(DbContextOptions<RentalMoviesDbContext> options):base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<RentalMovie> RentalMovies { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


    }
}
