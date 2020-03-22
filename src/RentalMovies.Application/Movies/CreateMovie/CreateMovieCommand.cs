using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.Movies.CreateMovie
{
    public class CreateMovieCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int NumberOfStocks { get; set; }
    }
}
