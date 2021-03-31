using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailys_UserProps_UserPropertyId",
                table: "TodoDailys");

            migrationBuilder.AlterColumn<int>(
                name: "UserPropertyId",
                table: "TodoDailys",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MadeUntil",
                table: "TodoDailys",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailys_UserProps_UserPropertyId",
                table: "TodoDailys",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoDailys_UserProps_UserPropertyId",
                table: "TodoDailys");

            migrationBuilder.AlterColumn<int>(
                name: "UserPropertyId",
                table: "TodoDailys",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MadeUntil",
                table: "TodoDailys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoDailys_UserProps_UserPropertyId",
                table: "TodoDailys",
                column: "UserPropertyId",
                principalTable: "UserProps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
