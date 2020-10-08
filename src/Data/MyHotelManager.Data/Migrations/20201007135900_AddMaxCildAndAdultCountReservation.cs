using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotelManager.Data.Migrations
{
    public partial class AddMaxCildAndAdultCountReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdultCount",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ChildCount",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultCount",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ChildCount",
                table: "Reservations");
        }
    }
}
