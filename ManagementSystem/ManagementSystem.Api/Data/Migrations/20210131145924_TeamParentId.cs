using Microsoft.EntityFrameworkCore.Migrations;

namespace ManagementSystem.Api.Data.Migrations
{
    public partial class TeamParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Team",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_ParentId",
                table: "Team",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Team_ParentId",
                table: "Team",
                column: "ParentId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Team_ParentId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_Team_ParentId",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Team");
        }
    }
}
