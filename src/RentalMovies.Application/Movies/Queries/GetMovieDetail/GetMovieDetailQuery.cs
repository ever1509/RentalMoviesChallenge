using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQuery:IRequest<SingleMovieDto>
    {
        public int MovieId { get; set; }
    }
}
