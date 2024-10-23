using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class addkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_doctorId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_patientId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_doctorId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_patientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "doctorId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientId",
                table: "Appointment");

            migrationBuilder.AlterColumn<int>(
                name: "appointmentId",
                table: "Appointment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_appointmentId",
                table: "Appointment",
                column: "appointmentId",
                principalTable: "Doctor",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_appointmentId",
                table: "Appointment",
                column: "appointmentId",
                principalTable: "Patient",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctor_appointmentId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_appointmentId",
                table: "Appointment");

            migrationBuilder.AlterColumn<int>(
                name: "appointmentId",
                table: "Appointment",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "doctorId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "patientId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_doctorId",
                table: "Appointment",
                column: "doctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patientId",
                table: "Appointment",
                column: "patientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctor_doctorId",
                table: "Appointment",
                column: "doctorId",
                principalTable: "Doctor",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_patientId",
                table: "Appointment",
                column: "patientId",
                principalTable: "Patient",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
