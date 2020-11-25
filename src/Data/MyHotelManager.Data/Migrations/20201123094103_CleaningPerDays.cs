namespace MyHotelManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CleaningPerDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "CleaningPerDays",
                table: "Hotels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CleaningPerDays",
                table: "Hotels");

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
