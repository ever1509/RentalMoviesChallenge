using System;
using System.Collections.Generic;
using System.Text;

namespace RentalMovies.Domain.Entities
{
    public class Stock
    {
        public Stock()
        {
            RentalMovies = new HashSet<RentalMovie>();
        }
        public int StockId { get; set; }
        public int MovieId { get; set; }
        public bool IsAvailable { get; set; }
        public Guid UniqueKey { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual ICollection<RentalMovie> RentalMovies { get; set; }
    }
}
