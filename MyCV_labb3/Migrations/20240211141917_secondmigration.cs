using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCV_labb3.Migrations
{
    /// <inheritdoc />
    public partial class secondmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Projects__Users_UserModelId",
                table: "_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK__Skills__Users_UserModelId",
                table: "_Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users",
                table: "_Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Skills",
                table: "_Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Projects",
                table: "_Projects");

            migrationBuilder.RenameTable(
                name: "_Users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "_Skills",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "_Projects",
                newName: "Projects");

            migrationBuilder.RenameIndex(
                name: "IX__Skills_UserModelId",
                table: "Skills",
                newName: "IX_Skills_UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX__Projects_UserModelId",
                table: "Projects",
                newName: "IX_Projects_UserModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_UserModelId",
                table: "Projects",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Users_UserModelId",
                table: "Skills",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_UserModelId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Users_UserModelId",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "_Users");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "_Skills");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "_Projects");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_UserModelId",
                table: "_Skills",
                newName: "IX__Skills_UserModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserModelId",
                table: "_Projects",
                newName: "IX__Projects_UserModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users",
                table: "_Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Skills",
                table: "_Skills",
                column: "SkillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Projects",
                table: "_Projects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK__Projects__Users_UserModelId",
                table: "_Projects",
                column: "UserModelId",
                principalTable: "_Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Skills__Users_UserModelId",
                table: "_Skills",
                column: "UserModelId",
                principalTable: "_Users",
                principalColumn: "Id");
        }
    }
}
