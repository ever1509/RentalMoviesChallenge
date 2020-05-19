using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.Commands.AddMovieLike
{
    public class AddMovieLikeCommandHandler:IRequestHandler<AddMovieLikeCommand,int>
    {
        private readonly IRentalMoviesDbContext _context;
        public AddMovieLikeCommandHandler(IRentalMoviesDbContext context)
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
            };
            _context.MovieLikes.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.MovieId;
        }
    }
}
