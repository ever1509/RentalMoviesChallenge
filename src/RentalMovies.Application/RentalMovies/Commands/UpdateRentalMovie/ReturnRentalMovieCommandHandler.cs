using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentalMovies.Application.Common.Interfaces;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Application.RentalMovies.Commands.UpdateRentalMovie
{
    class ReturnRentalMovieCommandHandler:IRequestHandler<ReturnRentalMovieCommand,bool>
    {
        private readonly IRentalMoviesDbContext _context;
        private const decimal PENALTY_VALUE = 12.50m;
        public ReturnRentalMovieCommandHandler(IRentalMoviesDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(ReturnRentalMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.RentalMovies.Where(e => e.StockId == request.StockId).FirstAsync(cancellationToken);

            if (entity == null)
                throw new Exception($"Entity \"{nameof(Stock)}\" ({request.StockId}) was not found.");

            return await HasPenalty(entity,cancellationToken);
        }
        private async Task<bool> HasPenalty(RentalMovie entity, CancellationToken cancellationToken)
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

            await _context.SaveChangesAsync(cancellationToken);

            return penalty;
        }
    }
}
