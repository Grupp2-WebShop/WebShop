using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.Migrations
{
    public partial class SeededData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Description", "ImageName", "Price", "ProductName" },
                values: new object[,]
                {
                    { 1, "Mario defies gravity.", "SuperMarioGalaxy.png", 150, "Super Mario Galaxy" },
                    { 2, "Prepare to die.", "DarkSoulsRemastered.png", 150, "Dark Souls Remastered" },
                    { 3, "Remake of Pokémon Diamond.", "PokémonBrilliantDiamond.png", 300, "Pokémon Brilliant Diamond" },
                    { 4, "An open world, Action RPG from Bethesda.", "Skyrim.png", 300, "The Elder Scrolls V: Skyrim" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
