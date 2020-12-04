namespace MyHotelManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EmailPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailPassword",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailPassword",
                table: "AboutUs");
        }
    }
}
