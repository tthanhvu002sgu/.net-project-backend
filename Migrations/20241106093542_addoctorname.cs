using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class addoctorname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "doctorName",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "doctorName",
                table: "Doctor");
        }
    }
}
