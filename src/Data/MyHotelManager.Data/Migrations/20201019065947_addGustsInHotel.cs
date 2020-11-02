using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotelManager.Data.Migrations
{
    public partial class addGustsInHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Guests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_HotelId",
                table: "Guests",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Hotels_HotelId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_HotelId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Guests");
        }
    }
}
