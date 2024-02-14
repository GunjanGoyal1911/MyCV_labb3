using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyCV_labb3.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK__Projects__Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "_Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK__Skills__Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "_Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX__Projects_UserModelId",
                table: "_Projects",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX__Skills_UserModelId",
                table: "_Skills",
                column: "UserModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_Projects");

            migrationBuilder.DropTable(
                name: "_Skills");

            migrationBuilder.DropTable(
                name: "_Users");
        }
    }
}
