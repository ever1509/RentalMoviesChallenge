using System;
using System.Collections.Generic;
using System.Text;

namespace RentalMovies.Domain.Entities
{
    public class MovieLike
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
