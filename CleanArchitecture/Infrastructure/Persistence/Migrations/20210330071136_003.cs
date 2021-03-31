using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoDailys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoDailyActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Check = table.Column<bool>(type: "bit", nullable: false),
                    MadeSince = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MadeUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserPropertyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoDailys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoDailys_UserProps_UserPropertyId",
                        column: x => x.UserPropertyId,
                        principalTable: "UserProps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TodoDailyHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckStatus = table.Column<bool>(type: "bit", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TodoDailyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoDailyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoDailyHistories_TodoDailys_TodoDailyId",
                        column: x => x.TodoDailyId,
                        principalTable: "TodoDailys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoDailyHistories_TodoDailyId",
                table: "TodoDailyHistories",
                column: "TodoDailyId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoDailys_UserPropertyId",
                table: "TodoDailys",
                column: "UserPropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoDailyHistories");

            migrationBuilder.DropTable(
                name: "TodoDailys");
        }
    }
}
