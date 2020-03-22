using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.UpdateMovie
{
    public class UpdateMovieCommand:IRequest
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<MovieLike> MovieLikes { get; set; }
    }
}
