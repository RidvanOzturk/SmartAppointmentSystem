using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Polyclinic",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_BranchId",
                table: "Doctors",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Branches_BranchId",
                table: "Doctors",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Branches_BranchId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_BranchId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Polyclinic",
                table: "Doctors",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
