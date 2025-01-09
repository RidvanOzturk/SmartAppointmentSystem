using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedMailtoEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Users",
                newName: "Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "Mail");
        }
    }
}
