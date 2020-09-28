namespace MyHotelManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SelectedHotelInUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedHotelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SelectedHotelId",
                table: "AspNetUsers",
                column: "SelectedHotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Hotels_SelectedHotelId",
                table: "AspNetUsers",
                column: "SelectedHotelId",
                principalTable: "Hotels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Hotels_SelectedHotelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SelectedHotelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SelectedHotelId",
                table: "AspNetUsers");
        }
    }
}
