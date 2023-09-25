using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DataLibrary.Migrations
{
    public partial class AddedTokenAndSeedValueForMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tokens",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<string>(type: "text", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    last_m_odified_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expiry_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tokens", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "menu_items",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Veg" },
                    { 2, "Non-veg" },
                    { 3, "Vegan" },
                    { 4, "Beverage" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tokens");

            migrationBuilder.DeleteData(
                table: "menu_items",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "menu_items",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "menu_items",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "menu_items",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
