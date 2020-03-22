using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Data;

namespace RentalMovies.Application.Movies.GetAllMoviesList
{
    public class GetlAllMoviesListQueryHandler:IRequestHandler<GetAllMoviesListQuery, MoviesListVm>
    {
        private readonly RentalMoviesDbContext _context;
        private readonly IMapper _mapper;

        public GetlAllMoviesListQueryHandler(RentalMoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MoviesListVm> Handle(GetAllMoviesListQuery request, CancellationToken cancellationToken)
        {
            //TODO: Handle movies getting the role from user and limit only availability
            List<MovieDto> movies;
            if (!string.IsNullOrEmpty(request.Filter))
            {

                movies = await _context.Movies
                    .Include(e => e.Stocks)
                    .Include(e => e.MovieLikes)
                    //.Where(e => e.Stocks.Any(s => s.IsAvailable))
                    .Where(e => e.Title.Contains(request.Filter))
                    .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
            else
            {
                movies = await _context.Movies
                    .Include(e => e.Stocks)
                    .Include(e => e.MovieLikes)
                    //.Where(e => e.Stocks.Any(s => s.IsAvailable))
                    .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }


            movies.OrderBy(m => m.Title)
                .ThenByDescending(m => m.NumberOfLikes);

            Pagination<MovieDto> pagedMovies = BuildPagination(movies, request.Page, request.PageSize);

            return new MoviesListVm()
            {
                Movies = pagedMovies,
                RoleId = request.RoleId
            };
        }
        private Pagination<MovieDto> BuildPagination(List<MovieDto> movies, int? page, int? pageSize)
        {
            int currentPage = page.Value;
            int currentPageSize = pageSize.Value;
            int totalm = movies.Count();

            movies = movies.Skip(currentPage * currentPageSize)
                .Take(currentPageSize).ToList();

            return new Pagination<MovieDto>
            {
                Page = currentPage,
                TotalCount = totalm,
                TotalPages = (int)Math.Ceiling((decimal)totalm / currentPageSize),
                Items = movies
            };
        }
    }
}
