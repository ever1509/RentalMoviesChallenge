using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Data.Configurations;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Data
{
    public class RentalMoviesDbContext:DbContext
    {
        public RentalMoviesDbContext(DbContextOptions<RentalMoviesDbContext> options)
        :base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<RentalMovie> RentalMovies { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieConfiguration());
            modelBuilder.ApplyConfiguration(new RentalMovieConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new MovieLikeConfiguration());
        }
    }
}
