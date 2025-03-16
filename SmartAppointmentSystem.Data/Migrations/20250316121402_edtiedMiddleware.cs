using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class edtiedMiddleware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusCode",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Logs");
        }
    }
}
