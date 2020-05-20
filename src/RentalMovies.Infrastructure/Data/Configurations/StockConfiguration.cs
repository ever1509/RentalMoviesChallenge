using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Infrastructure.Data.Configurations
{
    public class StockConfiguration:IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(e => e.StockId);
            builder.Property(e => e.IsAvailable).IsRequired();
            builder.Property(e => e.UniqueKey).IsRequired();

            builder.HasOne(e => e.Movie)
                .WithMany(d => d.Stocks)
                .HasForeignKey(e => e.MovieId)
                .HasConstraintName("FK_Movie_Stocks");
        }
    }
}
