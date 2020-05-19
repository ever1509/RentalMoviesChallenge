using MediatR;

namespace RentalMovies.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommand:IRequest
    {
        public int MovieId { get; set; }
    }
}
