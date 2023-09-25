using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLibrary.Migrations
{
    public partial class AddedItemDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    shelf_life_in_days = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_items_details", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "id", "category_id", "count", "description", "name", "shelf_life_in_days" },
                values: new object[,]
                {
                    { 1, 1, 2, "Paneer", "Veg Item 1", 1 },
                    { 2, 2, 2, "Non Veg", "Non Veg Item 1", 1 },
                    { 3, 3, 2, "Vegan", "Vegan 1", 1 },
                    { 4, 4, 2, "beverage", "Beverage 1", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "items_details");

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
