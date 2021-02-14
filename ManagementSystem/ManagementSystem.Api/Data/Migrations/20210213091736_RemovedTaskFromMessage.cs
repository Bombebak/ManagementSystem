using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class RemovedTaskFromMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Task_TaskId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_TaskId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                table: "Message",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_TaskId",
                table: "Message",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Task_TaskId",
                table: "Message",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
