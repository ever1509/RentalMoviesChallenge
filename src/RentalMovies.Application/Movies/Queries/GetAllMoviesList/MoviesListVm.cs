using System;
using System.Collections.Generic;
using System.Text;
using RentalMovies.Application.Common.Models;

namespace RentalMovies.Application.Movies.Queries.GetAllMoviesList
{
    public class MoviesListVm
    {
        public Pagination<MovieDto> Movies { get; set; }
        public int RoleId { get; set; }
    }
}
