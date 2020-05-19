using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandValidator:AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(e => e.Description).NotEmpty().WithMessage("It's necessary to introduce a description");
            RuleFor(e => e.NumberOfStocks).NotEmpty().WithMessage("It's necessary the number of stocks");
            RuleFor(e => e.RentalPrice).NotEmpty();
            RuleFor(e => e.SalePrice).NotEmpty();
            RuleFor(e => e.Title).NotEmpty().MaximumLength(50);
        }
    }
}
