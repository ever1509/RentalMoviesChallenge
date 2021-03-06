﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using RentalMovies.Application.Common.Mappings;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.Queries.GetAllMoviesList
{
    public class MovieDto :IMapFrom<MovieDto>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal RentalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int NumberOfStocks { get; set; }
        public int NumberOfLikes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieDto>()
                .ForMember(d => d.MovieId, opt => opt.MapFrom(e => e.MovieId))
                .ForMember(d => d.Title, opt => opt.MapFrom(e => e.Title))
                .ForMember(d => d.Description, opt => opt.MapFrom(e => e.Description))
                .ForMember(d => d.Image, opt => opt.MapFrom(e => e.Image))
                .ForMember(d => d.RentalPrice, opt => opt.MapFrom(e => e.RentalPrice))
                .ForMember(d => d.SalePrice, opt => opt.MapFrom(e => e.SalePrice))
                .ForMember(d => d.NumberOfStocks, opt => opt.MapFrom(e => e.Stocks.Count()))
                .ForMember(d => d.NumberOfLikes, opt => opt.MapFrom(e => e.MovieLikes.Count()));
        }
    }
}
