using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.RentalMovies.UpdateRentalMovie
{
    public class ReturnMovieCommand:IRequest<bool>
    {
        public int MovieId { get; set; }
        public int StockId { get; set; }
        public int UserId { get; set; }
    }
}
