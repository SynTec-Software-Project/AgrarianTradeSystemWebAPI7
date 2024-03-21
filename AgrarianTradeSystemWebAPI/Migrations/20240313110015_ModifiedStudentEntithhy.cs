using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgrarianTradeSystemWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedStudentEntithhy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Assigns_Status_Order",
                table: "Assigns");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Assigns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Assigns",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigns",
                table: "Assigns",
                columns: new[] { "OrderId", "Status" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Assigns_Status_Order",
                table: "Assigns",
                sql: "[Status] IN (SELECT [Status] FROM [Order])");
        }
    }
}
