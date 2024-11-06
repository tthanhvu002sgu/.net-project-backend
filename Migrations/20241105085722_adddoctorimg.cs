using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class adddoctorimg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "doctorImg",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctorImg",
                table: "Doctor");
        }
    }
}
