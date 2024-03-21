using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrarianTradeSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCreatedInitialTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Assigns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns",
                columns: new[] { "OrderId", "Status" });

            migrationBuilder.AddCheckConstraint(
                name: "FK_Assigns_Orders",
                table: "Assigns",
                sql: "[Status] = [Order].[Status]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns");

            migrationBuilder.DropCheckConstraint(
                name: "FK_Assigns_Orders",
                table: "Assigns");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assigns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns",
                column: "OrderId");
        }
    }
}
