using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoHangFire.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsModelUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "NameUser");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameUser",
                table: "Users",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IdUser",
                table: "Users",
                newName: "Id");
        }
    }
}
