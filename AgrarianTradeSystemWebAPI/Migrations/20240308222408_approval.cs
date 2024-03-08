using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrarianTradeSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class approval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Farmers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Couriers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Couriers");
        }
    }
}
