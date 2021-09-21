using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class ScreeningCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screening_Shows_MovieId",
                table: "Screening");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreeningUser_Screening_ScreeningsId",
                table: "ScreeningUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screening",
                table: "Screening");

            migrationBuilder.RenameTable(
                name: "Screening",
                newName: "Screenings");

            migrationBuilder.RenameIndex(
                name: "IX_Screening_MovieId",
                table: "Screenings",
                newName: "IX_Screenings_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screenings",
                table: "Screenings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_Shows_MovieId",
                table: "Screenings",
                column: "MovieId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreeningUser_Screenings_ScreeningsId",
                table: "ScreeningUser",
                column: "ScreeningsId",
                principalTable: "Screenings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_Shows_MovieId",
                table: "Screenings");

            migrationBuilder.DropForeignKey(
                name: "FK_ScreeningUser_Screenings_ScreeningsId",
                table: "ScreeningUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screenings",
                table: "Screenings");

            migrationBuilder.RenameTable(
                name: "Screenings",
                newName: "Screening");

            migrationBuilder.RenameIndex(
                name: "IX_Screenings_MovieId",
                table: "Screening",
                newName: "IX_Screening_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screening",
                table: "Screening",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Screening_Shows_MovieId",
                table: "Screening",
                column: "MovieId",
                principalTable: "Shows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScreeningUser_Screening_ScreeningsId",
                table: "ScreeningUser",
                column: "ScreeningsId",
                principalTable: "Screening",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
