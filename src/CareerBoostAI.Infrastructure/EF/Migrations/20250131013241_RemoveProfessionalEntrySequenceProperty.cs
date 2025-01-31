using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProfessionalEntrySequenceProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadReadModel_Candidates_CandidateId",
                table: "UploadReadModel");

            migrationBuilder.DropIndex(
                name: "IX_UploadReadModel_CandidateId",
                table: "UploadReadModel");

            migrationBuilder.DropColumn(
                name: "CandidateId",
                table: "UploadReadModel");

            migrationBuilder.DropColumn(
                name: "SequenceIndex",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "SequenceIndex",
                table: "Educations");

            migrationBuilder.AddColumn<string>(
                name: "CandidateEmail",
                table: "UploadReadModel",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "UploadReadModel",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Candidates_Email",
                table: "Candidates",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_UploadReadModel_CandidateEmail",
                table: "UploadReadModel",
                column: "CandidateEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadReadModel_Candidates_CandidateEmail",
                table: "UploadReadModel",
                column: "CandidateEmail",
                principalTable: "Candidates",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadReadModel_Candidates_CandidateEmail",
                table: "UploadReadModel");

            migrationBuilder.DropIndex(
                name: "IX_UploadReadModel_CandidateEmail",
                table: "UploadReadModel");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Candidates_Email",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CandidateEmail",
                table: "UploadReadModel");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "UploadReadModel");

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateId",
                table: "UploadReadModel",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<uint>(
                name: "SequenceIndex",
                table: "Experiences",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "SequenceIndex",
                table: "Educations",
                type: "int unsigned",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_UploadReadModel_CandidateId",
                table: "UploadReadModel",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadReadModel_Candidates_CandidateId",
                table: "UploadReadModel",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
