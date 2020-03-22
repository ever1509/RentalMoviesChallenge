using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using RentalMovies.Application.Movies.GetAllMoviesList;
using RentalMovies.Application.Movies.GetMovieDetail;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Mappings
{
    public  class RentalMoviesMapperConfiguration:Profile
    {
        public RentalMoviesMapperConfiguration()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(d=> d.MovieId, opt => opt.MapFrom(e=>e.MovieId))
                .ForMember(d => d.Title, opt => opt.MapFrom(e => e.Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(d => d.Image, opt => opt.MapFrom(e => e.Image))
                .ForMember(d => d.RentalPrice, opt => opt.MapFrom(e => e.RentalPrice))
                .ForMember(d => d.SalePrice, opt => opt.MapFrom(e => e.SalePrice))
                .ForMember(d => d.NumberOfStocks, opt => opt.MapFrom(e => e.Stocks.Count()))
                .ForMember(d => d.NumberOfLikes, opt => opt.MapFrom(e => e.MovieLikes.Count()));
            CreateMap<Movie, SingleMovieDto>()
                .ForMember(d => d.MovieId, opt => opt.MapFrom(e => e.MovieId))
                .ForMember(d => d.Title, opt => opt.MapFrom(e => e.Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(d => d.Image, opt => opt.MapFrom(e => e.Image))
                .ForMember(d => d.RentalPrice, opt => opt.MapFrom(e => e.RentalPrice))
                .ForMember(d => d.SalePrice, opt => opt.MapFrom(e => e.SalePrice));
        }
    }
}
