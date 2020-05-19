using MediatR;

namespace RentalMovies.Application.Movies.Commands.AddMovieLike
{
    public class AddMovieLikeCommand:IRequest<int>
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
