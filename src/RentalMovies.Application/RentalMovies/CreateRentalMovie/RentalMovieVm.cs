using System;
using System.Collections.Generic;
using System.Text;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Application.RentalMovies.CreateRentalMovie
{
    public class RentalMovieVm
    {
        public int MovieId { get; set; }
        public int StockId { get; set; }
        public int UserId { get; set; }
        public int Days { get; set; }
        public StatusMovie StatusMovie { get; set; }
    }
}
