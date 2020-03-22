using MediatR;

namespace RentalMovies.Application.Movies.DeleteMovie
{
    public class DeleteMovieCommand:IRequest<int>, IRequest<Unit>
    {
        public int MovieId { get; set; }
    }
}