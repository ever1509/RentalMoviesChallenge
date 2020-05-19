using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.RentalMovies.Commands.UpdateRentalMovie
{
    public class ReturnRentalMovieCommand:IRequest<bool>
    {
        public int MovieId { get; set; }
        public int StockId { get; set; }
        public int UserId { get; set; }
    }
}
