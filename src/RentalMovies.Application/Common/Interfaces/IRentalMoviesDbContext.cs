using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Common.Interfaces
{
    public interface IRentalMoviesDbContext
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<RentalMovie> RentalMovies { get; set; }
        DbSet<MovieLike> MovieLikes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
