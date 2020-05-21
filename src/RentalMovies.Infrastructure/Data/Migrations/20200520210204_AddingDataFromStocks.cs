using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalMovies.Infrastructure.Data.Migrations
{
    public partial class AddingDataFromStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (8,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (8,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (8,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (8,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (8,1,NEWID())");

            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (9,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (9,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (9,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (9,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (9,1,NEWID())");

            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (10,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (10,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (10,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (10,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (10,1,NEWID())");

            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (11,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (11,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (11,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (11,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (11,1,NEWID())");

            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (12,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (12,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (12,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (12,1,NEWID())");
            migrationBuilder.Sql("INSERT INTO [dbo].[Stocks] ([MovieId],[IsAvailable],[UniqueKey]) VALUES (12,1,NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [dbo].[Stocks]");
        }
    }
}
