using Microsoft.EntityFrameworkCore.Migrations;

namespace UserIdentity.Data.Migrations
{
    public partial class AddEducationInIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education",
                table: "AspNetUsers");
        }
    }
}
