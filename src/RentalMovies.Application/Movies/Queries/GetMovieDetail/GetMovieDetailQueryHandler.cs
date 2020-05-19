using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.Movies.Queries.GetMovieDetail
{
    public class GetMovieDetailQueryHandler:IRequestHandler<GetMovieDetailQuery,SingleMovieDto>
    {
        private readonly IRentalMoviesDbContext _context;
        private readonly IMapper _mapper;

        public GetMovieDetailQueryHandler(IRentalMoviesDbContext context, IMapper mapper)
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
