using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RentalMovies.Data;
using RentalMovies.Domain.Entities;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Application.RentalMovies.CreateRentalMovie
{
    public class RentalMovieCommandHandler:IRequestHandler<RentalMovieCommand>
    {
        private readonly RentalMoviesDbContext _context;
        private readonly ILogger<RentalMovieCommandHandler> _logger;

        public RentalMovieCommandHandler(RentalMoviesDbContext context, ILogger<RentalMovieCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Unit> Handle(RentalMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Rental Movie Info: UserId {request.RentalMovies.Select(rm => rm.UserId).First()}, Date: {DateTime.Now}, Quantity: {request.RentalMovies.Count()}");

            foreach (var req in request.RentalMovies)
            {
                var entity = new RentalMovie()
                {
                    UserId = req.UserId,
                    RentalDate = DateTime.Now,
                    StatusMovie = req.StatusMovie,
                    ReturnDate = CheckRentedMovie(req.StatusMovie, req.Days),
                    StockId = req.StockId
                };

                _context.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                await UpdateStock(req.StockId, cancellationToken, req.StatusMovie);
            }
            return Unit.Value;
        }
        private DateTime? CheckRentedMovie(StatusMovie statusMovie, int days)
        {
            if (statusMovie == StatusMovie.Rented)
                return DateTime.Now.AddDays(days);
            return null;
        }

        private async Task UpdateStock(int stockId, CancellationToken cancellationToken, StatusMovie statusMovie)
        {
            var stock = await _context.Stocks.FindAsync(stockId);
            stock.IsAvailable = false;

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
