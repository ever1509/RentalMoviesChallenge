using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalMovies.Domain.Entities;

namespace RentalMovies.Data.Configurations
{
    public class MovieLikeConfiguration:IEntityTypeConfiguration<MovieLike>
    {
        public void Configure(EntityTypeBuilder<MovieLike> builder)
        {
            builder.HasKey(e => new {e.MovieId,e.UserId});
            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.HasOne(e => e.Movie)
                .WithMany(d => d.MovieLikes)
                .HasForeignKey(e => e.MovieId)
                .HasConstraintName("FK_Movie_MovieLikes");
        }
    }
}
