using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCandidateIDReferenceOnCvTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Candidates_CandidateId",
                table: "Cvs");

            migrationBuilder.DropIndex(
                name: "IX_Cvs_CandidateId",
                table: "Cvs");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "Cvs");

            migrationBuilder.AlterColumn<string>(
                name: "CandidateEmail",
                table: "Cvs",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_CandidateEmail",
                table: "Cvs",
                column: "CandidateEmail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Candidates_CandidateEmail",
                table: "Cvs",
                column: "CandidateEmail",
                principalTable: "Candidates",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cvs_Candidates_CandidateEmail",
                table: "Cvs");

            migrationBuilder.DropIndex(
                name: "IX_Cvs_CandidateEmail",
                table: "Cvs");

            migrationBuilder.AlterColumn<string>(
                name: "CandidateEmail",
                table: "Cvs",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId",
                table: "Cvs",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Cvs_CandidateId",
                table: "Cvs",
                column: "CandidateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cvs_Candidates_CandidateId",
                table: "Cvs",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
