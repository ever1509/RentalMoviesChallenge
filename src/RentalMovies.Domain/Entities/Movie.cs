using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RentalMovies.Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
            Stocks= new HashSet<Stock>();
            MovieLikes= new HashSet<MovieLike>();
        }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
        public virtual ICollection<MovieLike> MovieLikes { get; set; }


    }

}
