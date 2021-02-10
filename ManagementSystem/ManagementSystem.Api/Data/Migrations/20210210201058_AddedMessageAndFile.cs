using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class AddedMessageAndFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApplicationMessageId",
                table: "Task",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    FileType = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Message_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Task_File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Task_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rel_Task_File_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_Task_File_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Message_File",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<long>(nullable: false),
                    FileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_Message_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rel_Message_File_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Rel_Message_File_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Task_Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<long>(nullable: false),
                    MessageId = table.Column<string>(nullable: true),
                    MessageId1 = table.Column<long>(nullable: true)
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
                name: "IX_Task_ApplicationMessageId",
                table: "Task",
                column: "ApplicationMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_File_UserId",
                table: "File",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ParentId",
                table: "Message",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Message_File_FileId",
                table: "Rel_Message_File",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Message_File_MessageId",
                table: "Rel_Message_File",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_File_FileId",
                table: "Rel_Task_File",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_File_TaskId",
                table: "Rel_Task_File",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_MessageId1",
                table: "Rel_Task_Message",
                column: "MessageId1");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_Task_Message_TaskId",
                table: "Rel_Task_Message",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Message_ApplicationMessageId",
                table: "Task",
                column: "ApplicationMessageId",
                principalTable: "Message",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Message_ApplicationMessageId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Rel_Message_File");

            migrationBuilder.DropTable(
                name: "Rel_Task_File");

            migrationBuilder.DropTable(
                name: "Rel_Task_Message");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Task_ApplicationMessageId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "ApplicationMessageId",
                table: "Task");
        }
    }
}
