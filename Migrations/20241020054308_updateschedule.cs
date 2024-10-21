using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class updateschedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Appointment_scheduleId",
                table: "Schedule");

            migrationBuilder.DropColumn(
                name: "appointmentDate",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "appointmentStatus",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "scheduleId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_scheduleId",
                table: "Appointment",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Schedule_scheduleId",
                table: "Appointment",
                column: "scheduleId",
                principalTable: "Schedule",
                principalColumn: "scheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Schedule_scheduleId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_scheduleId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "appointmentStatus",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "scheduleId",
                table: "Appointment");

            migrationBuilder.AddColumn<DateTime>(
                name: "appointmentDate",
                table: "Appointment",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Appointment",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Appointment_scheduleId",
                table: "Schedule",
                column: "scheduleId",
                principalTable: "Appointment",
                principalColumn: "appointmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
