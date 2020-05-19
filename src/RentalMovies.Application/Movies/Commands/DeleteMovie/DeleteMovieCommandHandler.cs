using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using RentalMovies.Application.Common.Interfaces;

namespace RentalMovies.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler:IRequestHandler<DeleteMovieCommand>
    {
        private readonly IRentalMoviesDbContext _context;
        public DeleteMovieCommandHandler(IRentalMoviesDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Movies.FindAsync(request.MovieId);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Movies)}\" ({request.MovieId}) was not found.");

            var hasStocks = _context.Stocks.Any(s => s.MovieId == request.MovieId && s.IsAvailable);

            if (hasStocks)
                throw new Exception("There are stocks associated with this movie.");

            _context.Movies.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
