using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidator:AbstractValidator<DeleteMovieCommand>
    {
        public DeleteMovieCommandValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty();
        }
    }
}
