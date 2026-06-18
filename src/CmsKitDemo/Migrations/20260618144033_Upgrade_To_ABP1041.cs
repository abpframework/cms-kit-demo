using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsKitDemo.Migrations
{
    /// <inheritdoc />
    public partial class Upgrade_To_ABP1041 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions");

            migrationBuilder.AddColumn<string>(
                name: "FrontChannelLogoutUri",
                table: "OpenIddictApplications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CmsPages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSignInTime",
                table: "AbpUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Leaved",
                table: "AbpUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "AbpPermissions",
                type: "TEXT",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<string>(
                name: "ManagementPermissionName",
                table: "AbpPermissions",
                type: "TEXT",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceName",
                table: "AbpPermissions",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AbpResourcePermissionGrants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderName = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 64, nullable: false),
                    ResourceName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ResourceKey = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpResourcePermissionGrants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserPasskeys",
                columns: table => new
                {
                    CredentialId = table.Column<byte[]>(type: "BLOB", maxLength: 1024, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserPasskeys", x => x.CredentialId);
                    table.ForeignKey(
                        name: "FK_AbpUserPasskeys_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserPasswordHistories",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    TenantId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserPasswordHistories", x => new { x.UserId, x.Password });
                    table.ForeignKey(
                        name: "FK_AbpUserPasswordHistories_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_ResourceName_Name",
                table: "AbpPermissions",
                columns: new[] { "ResourceName", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpResourcePermissionGrants_TenantId_Name_ResourceName_ResourceKey_ProviderName_ProviderKey",
                table: "AbpResourcePermissionGrants",
                columns: new[] { "TenantId", "Name", "ResourceName", "ResourceKey", "ProviderName", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserPasskeys_UserId",
                table: "AbpUserPasskeys",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpResourcePermissionGrants");

            migrationBuilder.DropTable(
                name: "AbpUserPasskeys");

            migrationBuilder.DropTable(
                name: "AbpUserPasswordHistories");

            migrationBuilder.DropIndex(
                name: "IX_AbpPermissions_ResourceName_Name",
                table: "AbpPermissions");

            migrationBuilder.DropColumn(
                name: "FrontChannelLogoutUri",
                table: "OpenIddictApplications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CmsPages");

            migrationBuilder.DropColumn(
                name: "LastSignInTime",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Leaved",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ManagementPermissionName",
                table: "AbpPermissions");

            migrationBuilder.DropColumn(
                name: "ResourceName",
                table: "AbpPermissions");

            migrationBuilder.AlterColumn<string>(
                name: "GroupName",
                table: "AbpPermissions",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_Name",
                table: "AbpPermissions",
                column: "Name",
                unique: true);
        }
    }
}
