using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.Movies.GetMovieDetail
{
    public class GetMovieDetailQuery:IRequest<SingleMovieDto>
    {
        public int MovieId { get; set; }
    }
}
