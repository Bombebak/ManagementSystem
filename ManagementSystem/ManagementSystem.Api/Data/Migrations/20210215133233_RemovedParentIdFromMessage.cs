using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class RemovedParentIdFromMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Message_ParentId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ParentId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Message",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ParentId",
                table: "Message",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Message_ParentId",
                table: "Message",
                column: "ParentId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
