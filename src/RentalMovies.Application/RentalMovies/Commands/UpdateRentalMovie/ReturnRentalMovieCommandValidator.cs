using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.RentalMovies.Commands.UpdateRentalMovie
{
    public class ReturnRentalMovieCommandValidator:AbstractValidator<ReturnRentalMovieCommand>
    {
        public ReturnRentalMovieCommandValidator()
        {
            RuleFor(e => e.StockId).NotEmpty();
            RuleFor(e => e.MovieId).NotEmpty();
            RuleFor(e => e.UserId).NotEmpty();
        }
    }
}
