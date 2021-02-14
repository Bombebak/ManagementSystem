using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class ReAddedTaskMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rel_Task_Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<long>(nullable: false),
                    MessageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Task_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rel_Task_Message_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_Task_Message_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_MessageId",
                table: "Rel_Task_Message",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_TaskId",
                table: "Rel_Task_Message",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rel_Task_Message");
        }
    }
}
