using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.Movies.Queries.GetAllMoviesList
{
    public class GetAllMoviesListQuery:IRequest<MoviesListVm>
    {
        public int RoleId { get; set; }

        //For pagination
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public string Filter { get; set; }
    }
}
