using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class removeCvIdFkOnCandidateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVId",
                table: "Candidates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CVId",
                table: "Candidates",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");
        }
    }
}
