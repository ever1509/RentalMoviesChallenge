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
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.GetMovieDetail
{
    public class GetMovieDetailQueryHandler:IRequestHandler<GetMovieDetailQuery,SingleMovieDto>
    {
        private readonly RentalMoviesDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryHandler(RentalMoviesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SingleMovieDto> Handle(GetMovieDetailQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Movies.Where(m => m.MovieId == request.MovieId)
                .ProjectTo<SingleMovieDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
            if (dto == null)
                throw new Exception($"Entity \"{nameof(Movie)}\" ({request.MovieId}) was not found.");

            return dto;
        }
    }
}
