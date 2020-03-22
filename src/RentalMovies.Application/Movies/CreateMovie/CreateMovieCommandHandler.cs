using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalMovies.Data;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.CreateMovie
{
    public class CreateMovieCommandHandler:IRequestHandler<CreateMovieCommand,int>
    {
        private readonly RentalMoviesDbContext _context;

        public CreateMovieCommandHandler(RentalMoviesDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = new Movie()
            {
                Title = request.Title,
                 Description = request.Description,
                 Image = request.Image,
                 RentalPrice = request.RentalPrice,
                 SalePrice = request.SalePrice,
                 MovieLikes = new List<MovieLike>()
            };
            _context.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await AddStocks(request.NumberOfStocks, entity.MovieId);

            return entity.MovieId;
        }

        private async Task AddStocks(int requestNumberOfStocks, int entityMovieId)
        {
            var stocks = new List<Stock>();

            for (int i = 0; i < requestNumberOfStocks; i++)
            {
                stocks.Add(new Stock{IsAvailable = true,MovieId = entityMovieId,UniqueKey = new Guid()});
            }

            await _context.AddRangeAsync(stocks);
            await _context.SaveChangesAsync();
        }
    }
}
