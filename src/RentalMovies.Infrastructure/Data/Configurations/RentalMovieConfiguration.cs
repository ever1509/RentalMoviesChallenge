using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Infrastructure.Data.Configurations
{
    public class RentalMovieConfiguration:IEntityTypeConfiguration<RentalMovie>
    {
        public void Configure(EntityTypeBuilder<RentalMovie> builder)
        {
            builder.HasKey(e => e.RentalMovieId);
            builder.Property(e => e.RentalDate).HasColumnType("datetime");
            builder.Property(e => e.ReturnDate).HasColumnType("datetime").IsRequired(false);
            builder.Property(e => e.StatusMovie).HasColumnType("int");
            builder.Property(e => e.PenaltyMoney).HasColumnType("decimal(18, 6)");
            builder.Property(e => e.IsPenaltySolved).HasDefaultValueSql("1");

            builder.HasOne(e => e.Stock)
                .WithMany(d => d.RentalMovies)
                .HasForeignKey(e => e.StockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_RentalMovies");
        }
    }
}
