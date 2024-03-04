using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrarianTradeSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class refreshTkn5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CropDetails",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GSLetterImg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LicenseImg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NICBackImg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NICFrontImg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VehicleImg",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VehicleNo",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Couriers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VehicleNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Couriers", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Couriers_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CropDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NICFrontImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NICBackImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSLetterImg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Farmers_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Couriers");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.AddColumn<string>(
                name: "CropDetails",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GSLetterImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NICBackImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NICFrontImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleImg",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
