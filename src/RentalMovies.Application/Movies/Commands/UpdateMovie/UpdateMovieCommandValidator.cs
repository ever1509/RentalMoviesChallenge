using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator:AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty().WithMessage("The Id is mandatory");
            RuleFor(e => e.Description).NotEmpty().WithMessage("It's necessary to introduce a description");
            RuleFor(e => e.Stocks).NotEmpty().WithMessage("It's necessary the number of stocks");
            RuleFor(e => e.RentalPrice).NotEmpty();
            RuleFor(e => e.SalePrice).NotEmpty();
            RuleFor(e => e.Title).NotEmpty().MaximumLength(50);

        }
    }
}
