using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class ok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Users_userId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "PatientRole");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_roleId",
                table: "PatientRole",
                newName: "IX_PatientRole_roleId");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Role",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "dob",
                table: "Patient",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Patient",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "dob",
                table: "Doctor",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientRole",
                table: "PatientRole",
                columns: new[] { "userId", "roleId" });

            migrationBuilder.CreateTable(
                name: "DoctorRole",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorRole", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK_DoctorRole_Doctor_userId",
                        column: x => x.userId,
                        principalTable: "Doctor",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorRole_Role_roleId",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "roleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Role_userId",
                table: "Role",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorRole_roleId",
                table: "DoctorRole",
                column: "roleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRole_Patient_userId",
                table: "PatientRole",
                column: "userId",
                principalTable: "Patient",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientRole_Role_roleId",
                table: "PatientRole",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Role_Users_userId",
                table: "Role",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientRole_Patient_userId",
                table: "PatientRole");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientRole_Role_roleId",
                table: "PatientRole");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_Users_userId",
                table: "Role");

            migrationBuilder.DropTable(
                name: "DoctorRole");

            migrationBuilder.DropIndex(
                name: "IX_Role_userId",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientRole",
                table: "PatientRole");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "dob",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "address",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "dob",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Doctor");

            migrationBuilder.RenameTable(
                name: "PatientRole",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_PatientRole_roleId",
                table: "UserRole",
                newName: "IX_UserRole_roleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "userId", "roleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_roleId",
                table: "UserRole",
                column: "roleId",
                principalTable: "Role",
                principalColumn: "roleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Users_userId",
                table: "UserRole",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
