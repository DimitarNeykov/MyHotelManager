namespace MyHotelManager.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddTourOperatorWithAgentAndCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenseExpireDate",
                table: "Hotels",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "TourOperatorAgents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOperatorAgents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourOperatorCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bulstat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOperatorCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourOperators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourOperatorAgentId = table.Column<int>(type: "int", nullable: false),
                    TourOperatorCompanyId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourOperators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourOperators_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourOperators_TourOperatorAgents_TourOperatorAgentId",
                        column: x => x.TourOperatorAgentId,
                        principalTable: "TourOperatorAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourOperators_TourOperatorCompanies_TourOperatorCompanyId",
                        column: x => x.TourOperatorCompanyId,
                        principalTable: "TourOperatorCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourOperatorAgents_IsDeleted",
                table: "TourOperatorAgents",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TourOperatorCompanies_IsDeleted",
                table: "TourOperatorCompanies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TourOperators_HotelId",
                table: "TourOperators",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_TourOperators_IsDeleted",
                table: "TourOperators",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_TourOperators_TourOperatorAgentId",
                table: "TourOperators",
                column: "TourOperatorAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_TourOperators_TourOperatorCompanyId",
                table: "TourOperators",
                column: "TourOperatorCompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourOperators");

            migrationBuilder.DropTable(
                name: "TourOperatorAgents");

            migrationBuilder.DropTable(
                name: "TourOperatorCompanies");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LicenseExpireDate",
                table: "Hotels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
