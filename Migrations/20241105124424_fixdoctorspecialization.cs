using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAn_API.Migrations
{
    /// <inheritdoc />
    public partial class fixdoctorspecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "specializationName",
                table: "Doctor",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "specializationName",
                table: "Doctor");
        }
    }
}
