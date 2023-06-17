using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcerPro.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TargetAppNotifiers");

            migrationBuilder.AddColumn<int>(
                name: "TargetAppId",
                table: "Notifiers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifiers_TargetAppId",
                table: "Notifiers",
                column: "TargetAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifiers_TargetApps_TargetAppId",
                table: "Notifiers",
                column: "TargetAppId",
                principalTable: "TargetApps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifiers_TargetApps_TargetAppId",
                table: "Notifiers");

            migrationBuilder.DropIndex(
                name: "IX_Notifiers_TargetAppId",
                table: "Notifiers");

            migrationBuilder.DropColumn(
                name: "TargetAppId",
                table: "Notifiers");

            migrationBuilder.CreateTable(
                name: "TargetAppNotifiers",
                columns: table => new
                {
                    TargetAppId = table.Column<int>(type: "int", nullable: false),
                    NotifierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetAppNotifiers", x => new { x.TargetAppId, x.NotifierId });
                    table.ForeignKey(
                        name: "FK_TargetAppNotifiers_Notifiers_NotifierId",
                        column: x => x.NotifierId,
                        principalTable: "Notifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetAppNotifiers_TargetApps_TargetAppId",
                        column: x => x.TargetAppId,
                        principalTable: "TargetApps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TargetAppNotifiers_NotifierId",
                table: "TargetAppNotifiers",
                column: "NotifierId");
        }
    }
}
