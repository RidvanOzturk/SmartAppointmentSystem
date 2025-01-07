using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedServiceEntityNameToProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_ProfessionalId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Services_ProfessionalId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Processes");

            migrationBuilder.RenameIndex(
                name: "IX_Services_ProfessionalId",
                table: "Processes",
                newName: "IX_Processes_ProfessionalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Processes_Users_ProfessionalId",
                table: "Processes",
                column: "ProfessionalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Processes_ProfessionalId",
                table: "TimeSlots",
                column: "ProfessionalId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Processes_Users_ProfessionalId",
                table: "Processes");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Processes_ProfessionalId",
                table: "TimeSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.RenameTable(
                name: "Processes",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_Processes_ProfessionalId",
                table: "Services",
                newName: "IX_Services_ProfessionalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_ProfessionalId",
                table: "Services",
                column: "ProfessionalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Services_ProfessionalId",
                table: "TimeSlots",
                column: "ProfessionalId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
