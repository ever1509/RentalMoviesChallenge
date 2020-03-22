using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Data;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.RentalMovies.UpdateRentalMovie
{
    public class ReturnMovieCommandHandler:IRequestHandler<ReturnMovieCommand,bool>
    {
        private readonly RentalMoviesDbContext _context;
        private const decimal PENALTY_VALUE = 12.50m;
        public ReturnMovieCommandHandler(RentalMoviesDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(ReturnMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.RentalMovies.Where(e => e.StockId == request.StockId).FirstAsync(cancellationToken);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Stock)}\" ({request.StockId}) was not found.");

            return await HasPenalty(entity);
        }
        private async Task<bool> HasPenalty(RentalMovie entity)
        {
            bool penalty = false;
            if (entity.ReturnDate < DateTime.Now)
            {
                penalty = true;
                entity.PenaltyMoney = PENALTY_VALUE;
                entity.IsPenaltySolved = false;
            }
            else
            {
                var stock = await _context.Stocks.FindAsync(entity.StockId);
                stock.IsAvailable = true;
            }

            await _context.SaveChangesAsync();

            return penalty;
        }
    }
}
