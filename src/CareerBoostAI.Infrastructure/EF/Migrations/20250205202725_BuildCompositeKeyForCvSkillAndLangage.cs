using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class BuildCompositeKeyForCvSkillAndLangage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                columns: new[] { "Id", "CvId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                columns: new[] { "Id", "CvId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");
        }
    }
}
