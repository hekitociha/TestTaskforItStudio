using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestTask.Migrations.AppSecondDBContentMigrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photoDescription",
                table: "isCopiedPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photoDescription",
                table: "isCopiedPhotos");
        }
    }
}
