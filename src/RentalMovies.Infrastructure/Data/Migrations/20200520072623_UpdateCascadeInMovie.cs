using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalMovies.Infrastructure.Data.Migrations
{
    public partial class UpdateCascadeInMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Stocks",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Stocks",
                table: "Stocks",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Stocks",
                table: "Stocks");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Stocks",
                table: "Stocks",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
