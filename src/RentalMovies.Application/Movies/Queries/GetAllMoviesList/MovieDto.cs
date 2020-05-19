using System;
using System.Collections.Generic;
using System.Text;

namespace RentalMovies.Application.Movies.Queries.GetAllMoviesList
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int NumberOfStocks { get; set; }
        public int NumberOfLikes { get; set; }
    }
}
