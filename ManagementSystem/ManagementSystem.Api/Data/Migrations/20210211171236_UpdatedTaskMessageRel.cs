using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class UpdatedTaskMessageRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rel_Task_Message_Task_TaskId",
                table: "Rel_Task_Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Message_ApplicationMessageId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_ApplicationMessageId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ApplicationMessageId",
                table: "Task");

            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                table: "Message",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_Task_Message_Task_TaskId",
                table: "Rel_Task_Message",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Task_TaskId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Rel_Task_Message_Task_TaskId",
                table: "Rel_Task_Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_TaskId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Message");

            migrationBuilder.AddColumn<long>(
                name: "ApplicationMessageId",
                table: "Task",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_ApplicationMessageId",
                table: "Task",
                column: "ApplicationMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rel_Task_Message_Task_TaskId",
                table: "Rel_Task_Message",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Message_ApplicationMessageId",
                table: "Task",
                column: "ApplicationMessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
