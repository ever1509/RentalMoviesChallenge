using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryValidator:AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(e => e.MovieId).NotEmpty();
        }
    }
}
