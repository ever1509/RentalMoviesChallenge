using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(e => e.MovieId);
            builder.Property(e => e.Title).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Description).HasColumnType("varchar(2000)").IsRequired();
            builder.Property(e => e.Image).HasColumnType("varchar(2000)").IsRequired(false);
            builder.Property(e => e.RentalPrice).HasColumnType("decimal(18, 6)");
            builder.Property(e => e.SalePrice).HasColumnType("decimal(18,6)");
        }
    }
}
