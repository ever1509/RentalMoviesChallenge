using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace RentalMovies.Application.Movies.AddMovieLike
{
    public class AddMovieLikeCommand:IRequest<int>
    {
        public int  MovieId { get; set; }
        public int UserId { get; set; }
    }
}
