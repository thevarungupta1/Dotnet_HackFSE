using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Outreach.Reporting.Data.Migrations
{
    public partial class test_ConstranitsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Events",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
