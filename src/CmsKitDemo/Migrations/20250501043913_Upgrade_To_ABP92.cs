using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsKitDemo.Migrations
{
    /// <inheritdoc />
    public partial class Upgrade_To_ABP92 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequiredPermissionName",
                table: "CmsMenuItems",
                type: "TEXT",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AbpRoles",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AbpClaimTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredPermissionName",
                table: "CmsMenuItems");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AbpRoles");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AbpClaimTypes");
        }
    }
}
