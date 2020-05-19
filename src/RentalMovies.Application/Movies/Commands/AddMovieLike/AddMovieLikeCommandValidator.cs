using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Commands.AddMovieLike
{
    public class AddMovieLikeCommandValidator:AbstractValidator<AddMovieLikeCommand>
    {
        public AddMovieLikeCommandValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty();
            RuleFor(e => e.UserId).NotEmpty();
        }
    }
}
