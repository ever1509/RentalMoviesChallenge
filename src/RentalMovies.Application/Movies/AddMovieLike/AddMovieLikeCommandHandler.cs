using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalMovies.Data;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.AddMovieLike
{
    public class AddMovieLikeCommandHandler:IRequestHandler<AddMovieLikeCommand,int>
    {
        private readonly RentalMoviesDbContext _context;

        public AddMovieLikeCommandHandler(RentalMoviesDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(AddMovieLikeCommand request, CancellationToken cancellationToken)
        {
            var entity = new MovieLike
            {
                MovieId = request.MovieId,
                CreatedDate = DateTime.Now,
                UserId = request.UserId
                //TODO handle the userId with Identity
            };
            _context.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.MovieId;
        }
    }
}
