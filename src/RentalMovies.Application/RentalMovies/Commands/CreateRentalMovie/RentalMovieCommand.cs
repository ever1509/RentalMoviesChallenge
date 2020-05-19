using System.Collections.Generic;
using MediatR;

namespace RentalMovies.Application.RentalMovies.Commands.CreateRentalMovie
{
    public class RentalMovieCommand:IRequest
    {
        public List<RentalMovieVm> RentalMovies { get; set; }
    }
}
