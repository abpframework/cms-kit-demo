using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CmsKitDemo.Migrations
{
    /// <inheritdoc />
    public partial class Added_Summary_To_GalleryImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentsSummary",
                table: "CmsDemoImages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentsSummary",
                table: "CmsDemoImages");
        }
    }
}
