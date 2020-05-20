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

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch (Exception e)
            {
                RollBackTransaction();
                Console.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
                
            }
        }

        private void RollBackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
