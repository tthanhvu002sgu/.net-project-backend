using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class addappointmentproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Appointment_appointmentId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_appointmentId",
                table: "Payments",
                newName: "IX_Payments_appointmentId");

            migrationBuilder.AddColumn<decimal>(
                name: "appointmentFee",
                table: "Appointment",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "patientName",
                table: "Appointment",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "patientEmail",
                table: "Payments",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "Payments",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "paymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Appointment_appointmentId",
                table: "Payments",
                column: "appointmentId",
                principalTable: "Appointment",
                principalColumn: "appointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Appointment_appointmentId",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "appointmentFee",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientName",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientEmail",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_appointmentId",
                table: "Payment",
                newName: "IX_Payment_appointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "paymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Appointment_appointmentId",
                table: "Payment",
                column: "appointmentId",
                principalTable: "Appointment",
                principalColumn: "appointmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
