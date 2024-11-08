using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class delschedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Schedule_scheduleId",
                table: "Appointment");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_scheduleId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "scheduleId",
                table: "Appointment");

            migrationBuilder.AddColumn<DateOnly>(
                name: "date",
                table: "Appointment",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "time",
                table: "Appointment",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "time",
                table: "Appointment");

            migrationBuilder.AddColumn<int>(
                name: "scheduleId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    scheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    doctorId = table.Column<int>(type: "int", nullable: false),
                    IsBooked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDoctorUnavailable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.scheduleId);
                    table.ForeignKey(
                        name: "FK_Schedule_Doctor_doctorId",
                        column: x => x.doctorId,
                        principalTable: "Doctor",
                        principalColumn: "doctorId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_scheduleId",
                table: "Appointment",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_doctorId",
                table: "Schedule",
                column: "doctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Schedule_scheduleId",
                table: "Appointment",
                column: "scheduleId",
                principalTable: "Schedule",
                principalColumn: "scheduleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
