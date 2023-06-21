using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talabat.Repository.Data.Migrations
{
    public partial class intialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureURL",
                table: "Products",
                newName: "PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Products",
                newName: "PictureURL");
        }
    }
}
