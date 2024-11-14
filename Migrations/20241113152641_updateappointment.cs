using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class updateappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "appointmentDescription",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "appointmentTitle",
                table: "Appointment",
                newName: "patientEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "patientEmail",
                table: "Appointment",
                newName: "appointmentTitle");

            migrationBuilder.AddColumn<string>(
                name: "appointmentDescription",
                table: "Appointment",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
