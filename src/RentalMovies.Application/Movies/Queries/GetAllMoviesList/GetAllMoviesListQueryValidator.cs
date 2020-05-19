using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace RentalMovies.Application.Movies.Queries.GetAllMoviesList
{
    public class GetAllMoviesListQueryValidator:AbstractValidator<GetAllMoviesListQuery>
    {
        public GetAllMoviesListQueryValidator()
        {
            RuleFor(e => e.Page).NotEmpty();
            RuleFor(e => e.PageSize).NotEmpty();
        }
    }
}
