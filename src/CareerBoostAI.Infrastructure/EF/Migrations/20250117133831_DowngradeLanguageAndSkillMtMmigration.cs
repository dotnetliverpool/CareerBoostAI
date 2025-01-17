using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareerBoostAI.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class DowngradeLanguageAndSkillMtMmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CvLanguage");

            migrationBuilder.DropTable(
                name: "CvSkill");

            migrationBuilder.DropTable(
                name: "Upload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Skills",
                newName: "CvId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Languages",
                newName: "CvId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "UploadReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Extension = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageMedium = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CandidateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadReadModel_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CvId",
                table: "Skills",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CvId",
                table: "Languages",
                column: "CvId");

            migrationBuilder.CreateIndex(
                name: "IX_UploadReadModel_CandidateId",
                table: "UploadReadModel",
                column: "CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Cvs_CvId",
                table: "Languages",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Cvs_CvId",
                table: "Skills",
                column: "CvId",
                principalTable: "Cvs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Cvs_CvId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Cvs_CvId",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "UploadReadModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CvId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_CvId",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Skills",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CvId",
                table: "Languages",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Skills",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Languages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CvLanguage",
                columns: table => new
                {
                    CvId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvLanguage", x => new { x.CvId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_CvLanguage_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CvLanguage_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CvSkill",
                columns: table => new
                {
                    CvId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SkillId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CvSkill", x => new { x.CvId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CvSkill_Cvs_CvId",
                        column: x => x.CvId,
                        principalTable: "Cvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CvSkill_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Upload",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CandidateId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Extension = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StorageMedium = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upload_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CvLanguage_LanguageId",
                table: "CvLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CvSkill_SkillId",
                table: "CvSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Upload_CandidateId",
                table: "Upload",
                column: "CandidateId");
        }
    }
}
