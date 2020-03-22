using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.RentalMovies.CreateRentalMovie
{
    public class RentalMovieCommand:IRequest
    {
        public List<RentalMovieVm> RentalMovies { get; set; }
    }
}
