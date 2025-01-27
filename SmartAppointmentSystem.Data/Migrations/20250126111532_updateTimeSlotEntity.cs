using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateTimeSlotEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Processes_ProfessionalId",
                table: "TimeSlots");

            migrationBuilder.AddColumn<Guid>(
                name: "ProcessId",
                table: "TimeSlots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_ProcessId",
                table: "TimeSlots",
                column: "ProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Processes_ProcessId",
                table: "TimeSlots",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Users_ProfessionalId",
                table: "TimeSlots",
                column: "ProfessionalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Processes_ProcessId",
                table: "TimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Users_ProfessionalId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_ProcessId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "ProcessId",
                table: "TimeSlots");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Processes_ProfessionalId",
                table: "TimeSlots",
                column: "ProfessionalId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
