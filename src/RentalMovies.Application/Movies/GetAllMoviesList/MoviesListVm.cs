using System;
using System.Collections.Generic;
using System.Text;

namespace RentalMovies.Application.Movies.GetAllMoviesList
{
    public class MoviesListVm
    {
        public Pagination<MovieDto> Movies { get; set; }
        public int RoleId { get; set; }
    }
}
