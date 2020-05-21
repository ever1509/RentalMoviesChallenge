using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalMovies.Infrastructure.Data.Migrations
{
    public partial class AddingDataFromMovies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Movies] ([Title],[Description],[Image],[RentalPrice],[SalePrice]) VALUES ('Star Wars Episode II Attack of the Clones','Ten years after the invasion of Naboo, the galaxy is on the brink of civil war. Under the leadership of a renegade Jedi named Count Dooku, thousands of solar systems threaten to break away from the Galactic Republic.','CloneWarsStarWars.jpg',5.50,15)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Movies] ([Title],[Description],[Image],[RentalPrice],[SalePrice]) VALUES ('Avengers: Endgame','The grave course of events set in motion by Thanos, that wiped out half the universe and fractured the Avengers ranks, compels the remaining Avengers to take one final stand in Marvel Studios grand conclusion to twenty-two films.','AvengerEndGame.jpg',7.50,15)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Movies] ([Title],[Description],[Image],[RentalPrice],[SalePrice]) VALUES ('Harry Potter and the prisoner of Azkaban','The book follows Harry Potter, a young wizard, in his third year at Hogwarts School of Witchcraft and Wizardry. Along with friends Ronald Weasley and Hermione Granger, Harry investigates Sirius Black, an escaped prisoner from Azkaban, the wizard prison, believed to be one of Lord Voldemort old allies.','HarryPotterThePrisonerAskaban.jpg',7.50,15)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Movies] ([Title],[Description],[Image],[RentalPrice],[SalePrice]) VALUES ('Prince of Persia: The Sands of Time','Set in the mystical lands of Persia, a rogue prince and a mysterious princess race against dark forces to safeguard an ancient dagger capable of releasing the Sands of Time -- a gift from the gods that can reverse time and allow its possessor to rule the world.','PrinceOfPersia.jpg',7.50,15)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Movies] ([Title],[Description],[Image],[RentalPrice],[SalePrice]) VALUES ('Mommy','The film follows adventurer Rick OConnell as he travels to Hamunaptra, the city of the dead, with a librarian and her brother, where they accidentally awaken Imhotep, a cursed high priest from the reign of the pharaoh Seti I.','TheMommy.jpg',7.50,15)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from [dbo].[Movies]");
        }
    }
}
