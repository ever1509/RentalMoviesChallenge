using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using RentalMovies.Data;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.UpdateMovie
{
    public class UpdateMovieCommandHandler:IRequestHandler<UpdateMovieCommand>
    {
        private readonly RentalMoviesDbContext _context;
        private readonly ILogger<UpdateMovieCommandHandler> _logger;
        public UpdateMovieCommandHandler(RentalMoviesDbContext context, ILogger<UpdateMovieCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies.FindAsync(request.MovieId);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Movie)}\" ({request.MovieId}) was not found.");

            _logger.LogInformation($"Update Movie Info: OldTitle:{entity.Title} / NewTitle:{request.Title}, OldRentalPrice: {entity.RentalPrice} / NewRentalPrice: {request.RentalPrice}, OldSalePrice: {entity.SalePrice} / NewSalePrice: {request.SalePrice}");

            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.RentalPrice = request.RentalPrice;
            entity.SalePrice = request.SalePrice;
            entity.Stocks = request.Stocks;
            entity.MovieLikes = request.MovieLikes;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
