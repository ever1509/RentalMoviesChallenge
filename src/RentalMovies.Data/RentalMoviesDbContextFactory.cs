using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RentalMovies.Data
{
    public class RentalMoviesDbContextFactory:IDesignTimeDbContextFactory<RentalMoviesDbContext>
    {
        public RentalMoviesDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentalMoviesDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Database=RentalMoviesDb;");

            return new RentalMoviesDbContext(optionsBuilder.Options);
        }
    }
}
