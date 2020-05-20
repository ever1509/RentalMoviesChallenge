using System;
using System.Collections.Generic;
using System.Text;
using RentalMovies.Domain.Enums;

namespace RentalMovies.Domain.Entities
{
    public class RentalMovie
    {
        public int RentalMovieId { get; set; }
        public int UserId { get; set; }
        public int StockId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public StatusMovie StatusMovie { get; set; }
        public decimal PenaltyMoney { get; set; }
        public bool? IsPenaltySolved { get; set; }

        public virtual Stock Stock { get; set; }
    }
}
