using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class DeletedTaskMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rel_Task_Message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rel_Task_Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId1 = table.Column<long>(type: "bigint", nullable: true),
                    TaskId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Task_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rel_Task_Message_Message_MessageId1",
                        column: x => x.MessageId1,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rel_Task_Message_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_MessageId1",
                table: "Rel_Task_Message",
                column: "MessageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_TaskId",
                table: "Rel_Task_Message",
                column: "TaskId");
        }
    }
}
