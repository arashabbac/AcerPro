using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcerPro.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetApp_Users_UserId",
                table: "TargetApp");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetAppNotifier_Notifier_NotifierId",
                table: "TargetAppNotifier");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetAppNotifier_TargetApp_TargetAppId",
                table: "TargetAppNotifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAppNotifier",
                table: "TargetAppNotifier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetApp",
                table: "TargetApp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifier",
                table: "Notifier");

            migrationBuilder.RenameTable(
                name: "TargetAppNotifier",
                newName: "TargetAppNotifiers");

            migrationBuilder.RenameTable(
                name: "TargetApp",
                newName: "TargetApps");

            migrationBuilder.RenameTable(
                name: "Notifier",
                newName: "Notifiers");

            migrationBuilder.RenameIndex(
                name: "IX_TargetAppNotifier_NotifierId",
                table: "TargetAppNotifiers",
                newName: "IX_TargetAppNotifiers_NotifierId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetApp_UserId",
                table: "TargetApps",
                newName: "IX_TargetApps_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAppNotifiers",
                table: "TargetAppNotifiers",
                columns: new[] { "TargetAppId", "NotifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetApps",
                table: "TargetApps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifiers",
                table: "Notifiers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAppNotifiers_Notifiers_NotifierId",
                table: "TargetAppNotifiers",
                column: "NotifierId",
                principalTable: "Notifiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAppNotifiers_TargetApps_TargetAppId",
                table: "TargetAppNotifiers",
                column: "TargetAppId",
                principalTable: "TargetApps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetApps_Users_UserId",
                table: "TargetApps",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetAppNotifiers_Notifiers_NotifierId",
                table: "TargetAppNotifiers");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetAppNotifiers_TargetApps_TargetAppId",
                table: "TargetAppNotifiers");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetApps_Users_UserId",
                table: "TargetApps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetApps",
                table: "TargetApps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetAppNotifiers",
                table: "TargetAppNotifiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifiers",
                table: "Notifiers");

            migrationBuilder.RenameTable(
                name: "TargetApps",
                newName: "TargetApp");

            migrationBuilder.RenameTable(
                name: "TargetAppNotifiers",
                newName: "TargetAppNotifier");

            migrationBuilder.RenameTable(
                name: "Notifiers",
                newName: "Notifier");

            migrationBuilder.RenameIndex(
                name: "IX_TargetApps_UserId",
                table: "TargetApp",
                newName: "IX_TargetApp_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TargetAppNotifiers_NotifierId",
                table: "TargetAppNotifier",
                newName: "IX_TargetAppNotifier_NotifierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetApp",
                table: "TargetApp",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetAppNotifier",
                table: "TargetAppNotifier",
                columns: new[] { "TargetAppId", "NotifierId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifier",
                table: "Notifier",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetApp_Users_UserId",
                table: "TargetApp",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAppNotifier_Notifier_NotifierId",
                table: "TargetAppNotifier",
                column: "NotifierId",
                principalTable: "Notifier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetAppNotifier_TargetApp_TargetAppId",
                table: "TargetAppNotifier",
                column: "TargetAppId",
                principalTable: "TargetApp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
