using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QUS.Services.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Services",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Services");
        }
    }
}
