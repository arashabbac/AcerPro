using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcerPro.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToTargetApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TargetApps",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TargetApps");
        }
    }
}
