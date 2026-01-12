using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWDWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class Updateimagestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Images",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Images",
                newName: "FileDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FileDescription",
                table: "Images",
                newName: "Description");
        }
    }
}
