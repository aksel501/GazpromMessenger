using Microsoft.EntityFrameworkCore.Migrations;

namespace GazpromMessenger.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Searches",
                table: "Searches");

            migrationBuilder.RenameTable(
                name: "Searches",
                newName: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Messages",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Searches");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Searches",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Searches",
                table: "Searches",
                column: "TaskID");
        }
    }
}
