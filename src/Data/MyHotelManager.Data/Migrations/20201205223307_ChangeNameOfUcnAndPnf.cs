namespace MyHotelManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeNameOfUcnAndPnf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UCN",
                table: "Guests",
                newName: "UniqueNumberForeigner");

            migrationBuilder.RenameColumn(
                name: "PNF",
                table: "Guests",
                newName: "IdentificationNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UniqueNumberForeigner",
                table: "Guests",
                newName: "UCN");

            migrationBuilder.RenameColumn(
                name: "IdentificationNumber",
                table: "Guests",
                newName: "PNF");
        }
    }
}
