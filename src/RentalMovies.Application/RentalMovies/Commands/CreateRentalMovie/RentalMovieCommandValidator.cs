using FluentValidation;

namespace RentalMovies.Application.RentalMovies.Commands.CreateRentalMovie
{
    public class RentalMovieCommandValidator:AbstractValidator<RentalMovieCommand>
    {
        public RentalMovieCommandValidator()
        {
            RuleFor(e => e.RentalMovies).NotEmpty();

        }
    }
}
