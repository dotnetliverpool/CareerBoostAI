using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateEmailColumnToCvTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CandidateEmail",
                table: "Cvs",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CandidateEmail",
                table: "Cvs");
        }
    }
}
